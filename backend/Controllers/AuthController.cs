using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, User> _users = new();
        private static int _userId = 1;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_users.ContainsKey(model.Email))
                return Conflict(new { message = "El email ya está registrado." });

            var user = new User
            {
                Id = _userId++,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = _authService.HashPassword(model.Password)
            };
            _users[model.Email] = user;
            return Ok(new { message = "Registro exitoso" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_users.TryGetValue(model.Email, out var user))
                return Unauthorized(new { message = "Credenciales inválidas." });

            if (!_authService.VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized(new { message = "Credenciales inválidas." });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
