using System;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthControllerV1 : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthControllerV1> _logger;

        public AuthControllerV1(IAuthService authService, ILogger<AuthControllerV1> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login: Solicitud con datos inválidos de {Email}", request.Email ?? "unknown");
                return BadRequest();
            }

            _logger.LogInformation("Login: Intento de acceso para {Email}", request.Email);
            var result = await _authService.LoginAsync(request);
            SetRefreshTokenCookie(result.RefreshToken);
            result.RefreshToken = string.Empty;
            _logger.LogInformation("Login: Acceso exitoso para usuario {UserId}", result.User?.Id);
            return Ok(result);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(RefreshResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogWarning("Refresh: Intento sin refresh token válido");
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://example.com/errors/invalid-refresh-token",
                    Title = "Invalid Refresh Token",
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = "Refresh token no proporcionado o expirado"
                });
            }

            var request = new RefreshRequestDto { RefreshToken = refreshToken };
            var result = await _authService.RefreshAsync(request);
            
            SetRefreshTokenCookie(result.RefreshToken);
            result.RefreshToken = string.Empty;
            _logger.LogInformation("Refresh: Token renovado exitosamente");
            return Ok(result);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var request = new LogoutRequestDto { RefreshToken = refreshToken };
                try
                {
                    await _authService.LogoutAsync(request);
                    _logger.LogInformation("Logout: Sesión cerrada exitosamente");
                }
                catch (Exception ex)
                {
                    // Log pero continúa para limpiar cookie de todas formas
                    _logger.LogWarning(ex, "Logout: Error durante revocación de token, pero se procede a limpiar cookie");
                }
            }

            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // HTTPS required in prod
                SameSite = SameSiteMode.Strict
            });

            return NoContent();
        }

        private void SetRefreshTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}