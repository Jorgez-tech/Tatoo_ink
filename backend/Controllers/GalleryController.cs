using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/gallery")]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IGalleryService galleryService, ILogger<GalleryController> logger)
        {
            _galleryService = galleryService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las imágenes públicas de la galería
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryImage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get: Consultando galería pública");
            var images = await _galleryService.GetAllImagesAsync(onlyPublic: true);
            return Ok(images);
        }

        /// <summary>
        /// Obtiene todas las imágenes (incluyendo ocultas) para administración
        /// </summary>
        [HttpGet("admin")]
        [Authorize(Roles = "admin,artist")]
        [ProducesResponseType(typeof(IEnumerable<GalleryImage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdmin()
        {
            _logger.LogInformation("GetAdmin: Admin consultando galería completa");
            var images = await _galleryService.GetAllImagesAsync(onlyPublic: false);
            return Ok(images);
        }

        /// <summary>
        /// Crea una nueva imagen en la galería
        /// </summary>
        [HttpPost("admin")]
        [Authorize(Roles = "admin,artist")]
        [ProducesResponseType(typeof(GalleryImage), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] GalleryImageCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create: Solicitud con datos inválidos");
                return BadRequest();
            }

            _logger.LogInformation("Create: Admin creando nueva imagen en galería");
            var image = await _galleryService.CreateImageAsync(request);
            return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
        }

        /// <summary>
        /// Actualiza una imagen existente
        /// </summary>
        [HttpPatch("admin/{id}")]
        [Authorize(Roles = "admin,artist")]
        [ProducesResponseType(typeof(GalleryImage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] GalleryImageUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update: Solicitud con datos inválidos para imagen ID {ImageId}", id);
                return BadRequest();
            }

            _logger.LogInformation("Update: Admin actualizando imagen ID {ImageId}", id);
            var image = await _galleryService.UpdateImageAsync(id, request);
            if (image == null)
            {
                _logger.LogWarning("Update: Imagen ID {ImageId} no encontrada", id);
                return NotFound();
            }
            return Ok(image);
        }

        /// <summary>
        /// Elimina una imagen de la galería
        /// </summary>
        [HttpDelete("admin/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete: Admin eliminando imagen ID {ImageId}", id);
            var success = await _galleryService.DeleteImageAsync(id);
            if (!success)
            {
                _logger.LogWarning("Delete: Imagen ID {ImageId} no encontrada", id);
                return NotFound();
            }
            return NoContent();
        }
    }
}
