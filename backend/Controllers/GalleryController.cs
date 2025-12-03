using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        /// Obtiene todas las imágenes de la galería
        /// </summary>
        /// <returns>Lista de imágenes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryImage>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var images = await _galleryService.GetAllImagesAsync();
                return Ok(images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener imágenes de la galería");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
