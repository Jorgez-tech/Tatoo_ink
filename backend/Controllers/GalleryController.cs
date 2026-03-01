using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly IFileStorageService _fileStorage;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IGalleryService galleryService, IFileStorageService fileStorage, ILogger<GalleryController> logger)
        {
            _galleryService = galleryService;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        /// <summary>Lista pública de todas las imágenes de la galería.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryImageResponse>), StatusCodes.Status200OK)]
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

        /// <summary>Imágenes de un artista específico (pública).</summary>
        [HttpGet("artist/{artistId:int}")]
        [ProducesResponseType(typeof(IEnumerable<GalleryImageResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByArtist(int artistId) =>
            Ok(await _galleryService.GetByArtistAsync(artistId));

        /// <summary>Subir nueva imagen. Solo Artist o Admin autenticados.</summary>
        [HttpPost]
        [Authorize(Roles = "Artist,Admin")]
        [ProducesResponseType(typeof(GalleryImageResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateGalleryImageRequest request, IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest(new { error = "Se requiere una imagen." });

            // Obtener artistId del token JWT
            var artistIdClaim = User.FindFirst("artistId")?.Value;
            int? artistId = artistIdClaim != null ? int.Parse(artistIdClaim) : null;

            string imagePath;
            try
            {
                if (!artistId.HasValue)
                    return BadRequest(new { error = "El usuario no tiene un artista asociado." });

                imagePath = await _fileStorage.SaveAsync(image, artistId.Value);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            var created = await _galleryService.CreateAsync(request, imagePath, artistId);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>Actualizar datos (alt, categoría, descripción) de una imagen. Solo el propietario o Admin.</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Artist,Admin")]
        [ProducesResponseType(typeof(GalleryImageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGalleryImageRequest request)
        {
            // Validar ownership antes de actualizar
            var image = await _galleryService.GetEntityByIdAsync(id);
            if (image == null) return NotFound();

            var isAdmin = User.IsInRole("Admin");
            var artistIdClaim = User.FindFirst("artistId")?.Value;
            var callerArtistId = artistIdClaim != null ? int.Parse(artistIdClaim) : (int?)null;

            if (!isAdmin && image.ArtistId != callerArtistId)
                return Forbid();

            var updated = await _galleryService.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        /// <summary>Eliminar imagen. Solo el propietario o Admin.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Artist,Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            var artistIdClaim = User.FindFirst("artistId")?.Value;
            var callerArtistId = artistIdClaim != null ? int.Parse(artistIdClaim) : (int?)null;

            // Antes de borrar de DB, obtener la ruta del archivo
            var image = await _galleryService.GetEntityByIdAsync(id);
            if (image == null) return NotFound();

            var deleted = await _galleryService.DeleteAsync(id, callerArtistId, isAdmin);
            if (!deleted) return Forbid();

            // Eliminar archivo físico si existe
            if (!string.IsNullOrEmpty(image.Src))
                await _fileStorage.DeleteAsync(image.Src);

            return NoContent();
        }
    }
}
