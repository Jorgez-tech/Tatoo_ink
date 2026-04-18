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

        public BusinessSettingsController(IBusinessSettingsService businessSettingsService)
        {
            _businessSettingsService = businessSettingsService;
        }

        [HttpGet("business-settings")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPublic()
        {
            var settings = await _businessSettingsService.GetAsync();
            return Ok(settings);
        }

        [HttpGet("internal/business-settings")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInternal()
        {
            var settings = await _businessSettingsService.GetInternalAsync();
            return Ok(settings);
        }

        [HttpPatch("internal/business-settings")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(BusinessSettingsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] BusinessSettingsUpdateDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _ = int.TryParse(userIdClaim, out var userId);
            var updated = await _businessSettingsService.UpdateAsync(request, userId > 0 ? userId : null);
            return Ok(updated);
        }
    }
}
