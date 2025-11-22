using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] object model)
        {
            // Lógica de registro (se implementará en el siguiente paso)
            return Ok(new { message = "Registro exitoso" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] object model)
        {
            // Lógica de login (se implementará en el siguiente paso)
            return Ok(new { token = "jwt-token" });
        }
    }
}
