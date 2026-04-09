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
        public async Task<IActionResult> Get()
        {
            var images = await _galleryService.GetAllImagesAsync(onlyPublic: true);
            return Ok(images);
        }

        /// <summary>
        /// Obtiene todas las imágenes (incluyendo ocultas) para administración
        /// </summary>
        [HttpGet("admin")]
        [Authorize(Roles = "admin,artist")]
        [ProducesResponseType(typeof(IEnumerable<GalleryImage>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdmin()
        {
            var images = await _galleryService.GetAllImagesAsync(onlyPublic: false);
            return Ok(images);
        }

        /// <summary>
        /// Crea una nueva imagen en la galería
        /// </summary>
        [HttpPost("admin")]
        [Authorize(Roles = "admin,artist")]
        public async Task<IActionResult> Create([FromBody] GalleryImageCreateDto request)
        {
            var image = await _galleryService.CreateImageAsync(request);
            return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
        }

        /// <summary>
        /// Actualiza una imagen existente
        /// </summary>
        [HttpPatch("admin/{id}")]
        [Authorize(Roles = "admin,artist")]
        public async Task<IActionResult> Update(int id, [FromBody] GalleryImageUpdateDto request)
        {
            var image = await _galleryService.UpdateImageAsync(id, request);
            if (image == null) return NotFound();
            return Ok(image);
        }

        /// <summary>
        /// Elimina una imagen de la galería
        /// </summary>
        [HttpDelete("admin/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _galleryService.DeleteImageAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
