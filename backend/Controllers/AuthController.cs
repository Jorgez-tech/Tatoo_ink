using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        // Vigencia del refresh token: 30 días
        private static readonly TimeSpan RefreshTokenExpiry = TimeSpan.FromDays(30);

        public AuthController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ApplicationDbContext context,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        }

        /// <summary>Login de usuario. Retorna JWT + refresh token.</summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", request.Email);
                return Unauthorized(new { error = "Credenciales inválidas." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Artist";
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.UserId == user.Id);

            var accessToken = _tokenService.GenerateAccessToken(user, role, artist?.Id);
            var refreshTokenValue = _tokenService.GenerateRefreshToken();
            var tokenHash = HashToken(refreshTokenValue);

            // Revocar tokens anteriores del usuario (sesión única por usuario)
            var existing = _context.RefreshTokens.Where(t => t.UserId == user.Id && !t.IsRevoked);
            foreach (var t in existing) t.IsRevoked = true;

            // Guardar nuevo refresh token (solo el hash, nunca el valor real)
            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId    = user.Id,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.Add(RefreshTokenExpiry),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            _logger.LogInformation("User {Email} logged in with role {Role}", user.Email, role);

            return Ok(new LoginResponse(
                Token:        accessToken,
                RefreshToken: refreshTokenValue,
                Email:        user.Email ?? "",
                Role:         role,
                ArtistId:     artist?.Id,
                DisplayName:  artist?.DisplayName
            ));
        }

        /// <summary>Renueva el access token usando el refresh token.</summary>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var tokenHash = HashToken(request.RefreshToken);

            // Buscar por hash — valida también que no esté revocado ni expirado
            var stored = await _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t =>
                    t.TokenHash == tokenHash &&
                    !t.IsRevoked &&
                    t.ExpiresAt > DateTime.UtcNow);

            if (stored == null)
            {
                _logger.LogWarning("Refresh token inválido, expirado o revocado.");
                return Unauthorized(new { error = "Refresh token inválido o expirado." });
            }

            var user = stored.User;
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Artist";
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.UserId == user.Id);

            var newAccessToken = _tokenService.GenerateAccessToken(user, role, artist?.Id);
            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();
            var newTokenHash = HashToken(newRefreshTokenValue);

            // Rotación: revocar el token actual y emitir uno nuevo
            stored.IsRevoked = true;

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId    = user.Id,
                TokenHash = newTokenHash,
                ExpiresAt = DateTime.UtcNow.Add(RefreshTokenExpiry),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            _logger.LogInformation("Refresh token rotado para usuario {Email}", user.Email);

            return Ok(new LoginResponse(
                Token:        newAccessToken,
                RefreshToken: newRefreshTokenValue,
                Email:        user.Email ?? "",
                Role:         role,
                ArtistId:     artist?.Id,
                DisplayName:  artist?.DisplayName
            ));
        }

        /// <summary>
        /// Calcula el hash SHA-256 de un token en representación hex lowercase.
        /// SHA-256 produce siempre 64 caracteres hex — coincide con la columna TokenHash(64).
        /// </summary>
        private static string HashToken(string token)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}

