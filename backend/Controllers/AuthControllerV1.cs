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
    }
}