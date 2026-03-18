using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthControllerV1 : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthControllerV1(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
            {
                return Unauthorized(new AuthErrorResponseDto
                {
                    Error = new AuthErrorDto
                    {
                        Code = "INVALID_CREDENTIALS",
                        Message = "Email o contraseña incorrectos"
                    }
                });
            }

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
            var result = await _authService.RefreshAsync(request);
            if (result == null)
            {
                return Unauthorized(new AuthErrorResponseDto
                {
                    Error = new AuthErrorDto
                    {
                        Code = "REFRESH_TOKEN_INVALID",
                        Message = "Refresh token invalido o expirado"
                    }
                });
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            var revoked = await _authService.LogoutAsync(request);
            if (!revoked)
            {
                return Unauthorized(new AuthErrorResponseDto
                {
                    Error = new AuthErrorDto
                    {
                        Code = "REFRESH_TOKEN_INVALID",
                        Message = "Refresh token invalido o expirado"
                    }
                });
            }

            return NoContent();
        }
    }
}