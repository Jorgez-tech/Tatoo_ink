using System;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(
            ApplicationDbContext context,
            IPasswordService passwordService,
            ITokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !user.IsActive)
            {
                return null;
            }

            if (!_passwordService.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            user.UpdatedAt = DateTime.UtcNow;
            var refreshToken = _tokenService.CreateRefreshToken();
            var refreshTokenDays = 7;

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenDays),
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                Token = _tokenService.CreateAccessToken(user),
                RefreshToken = refreshToken,
                User = new LoginUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }

        public async Task<RefreshResponseDto?> RefreshAsync(RefreshRequestDto request)
        {
            var storedRefresh = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (storedRefresh == null || !storedRefresh.IsActive || !storedRefresh.User.IsActive)
            {
                return null;
            }

            storedRefresh.RevokedAt = DateTime.UtcNow;
            storedRefresh.User.UpdatedAt = DateTime.UtcNow;

            var newRefreshToken = _tokenService.CreateRefreshToken();
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = storedRefresh.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return new RefreshResponseDto
            {
                Token = _tokenService.CreateAccessToken(storedRefresh.User),
                RefreshToken = newRefreshToken
            };
        }

        public async Task<bool> LogoutAsync(LogoutRequestDto request)
        {
            var storedRefresh = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (storedRefresh == null || !storedRefresh.IsActive)
            {
                return false;
            }

            storedRefresh.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}