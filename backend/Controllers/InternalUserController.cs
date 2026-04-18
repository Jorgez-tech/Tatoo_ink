using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/internal/users")]
    [Authorize(Roles = "admin")]
    public class InternalUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<InternalUserController> _logger;

        public InternalUserController(
            ApplicationDbContext context,
            IPasswordService passwordService,
            ILogger<InternalUserController> logger)
        {
            _context = context;
            _passwordService = passwordService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            _logger.LogInformation("GetAll: Admin consultando todos los usuarios");
            var users = await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            _logger.LogInformation("GetById: Admin consultando usuario ID {UserId}", id);
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("GetById: Usuario ID {UserId} no encontrado", id);
                return NotFound();
            }

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create: Solicitud con datos inválidos");
                return BadRequest();
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email.ToLower()))
            {
                _logger.LogWarning("Create: Intento de crear usuario con email duplicado {Email}", request.Email);
                return BadRequest();
            }

            var user = new User
            {
                Email = request.Email.ToLower(),
                PasswordHash = _passwordService.HashPassword(request.Password),
                Role = request.Role,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Create: Nuevo usuario creado {Email} con rol {Role}", user.Email, user.Role);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update: Solicitud con datos inválidos para usuario ID {UserId}", id);
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Update: Usuario ID {UserId} no encontrado", id);
                return NotFound();
            }

            if (request.Email != null)
            {
                var emailLower = request.Email.ToLower();
                if (emailLower != user.Email && await _context.Users.AnyAsync(u => u.Email == emailLower))
                {
                    _logger.LogWarning("Update: Intento de cambiar email a uno duplicado {Email} para usuario ID {UserId}", request.Email, id);
                    return BadRequest();
                }
                user.Email = emailLower;
            }

            if (request.Password != null)
            {
                user.PasswordHash = _passwordService.HashPassword(request.Password);
            }

            if (request.Role != null)
            {
                user.Role = request.Role;
            }

            if (request.IsActive.HasValue)
            {
                user.IsActive = request.IsActive.Value;
            }

            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Update: Usuario ID {UserId} actualizado correctamente", user.Id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Delete: Intento de eliminar usuario ID {UserId} inexistente", id);
                return NotFound();
            }

            // No permitir auto-eliminación por seguridad
            var currentUserIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(currentUserIdStr, out var currentUserId) && currentUserId == id)
            {
                _logger.LogWarning("Delete: Admin {UserId} intentó auto-eliminarse", id);
                return BadRequest(new ProblemDetails
                {
                    Type = "https://example.com/errors/self-deletion-prevented",
                    Title = "Self Deletion Not Allowed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "No puedes eliminar tu propio usuario administrador"
                });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete: Usuario ID {UserId} eliminado correctamente", id);

            return NoContent();
        }
    }
}
