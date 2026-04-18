using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class BusinessSettingsController : ControllerBase
    {
        private readonly IBusinessSettingsService _businessSettingsService;
        private readonly ILogger<BusinessSettingsController> _logger;

        public BusinessSettingsController(
            IBusinessSettingsService businessSettingsService,
            ILogger<BusinessSettingsController> logger)
        {
            _businessSettingsService = businessSettingsService;
            _logger = logger;
        }

        [HttpGet("business-settings")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPublic()
        {
            _logger.LogInformation("GetPublic: Consultando configuración pública del negocio");
            var settings = await _businessSettingsService.GetAsync();
            return Ok(settings);
        }

        [HttpGet("internal/business-settings")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInternal()
        {
            _logger.LogInformation("GetInternal: Admin consultando configuración interna del negocio");
            var settings = await _businessSettingsService.GetInternalAsync();
            return Ok(settings);
        }

        [HttpPatch("internal/business-settings")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] BusinessSettingsUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update: Solicitud con datos inválidos");
                return BadRequest();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                _logger.LogWarning("Update: No se pudo extraer userId válido del claim: {Claim}", userIdClaim ?? "null");
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://example.com/errors/invalid-user-context",
                    Title = "Invalid User Context",
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = "No se pudo verificar la identidad del usuario"
                });
            }

            _logger.LogInformation("Update: Admin {UserId} actualizando configuración del negocio", userId);
            var updated = await _businessSettingsService.UpdateAsync(request, userId);
            return Ok(updated);
        }
    }
}
