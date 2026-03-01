using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IFileStorageService _fileStorage;
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(IArtistService artistService, IFileStorageService fileStorage, ILogger<ArtistController> logger)
        {
            _artistService = artistService;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        /// <summary>Lista pública de artistas del estudio.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtistResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await _artistService.GetAllAsync());

        /// <summary>Perfil público de un artista.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ArtistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var artist = await _artistService.GetByIdAsync(id);
            return artist is null ? NotFound() : Ok(artist);
        }

        /// <summary>Actualizar perfil del artista (el artista actualiza el suyo; Admin puede actualizar cualquiera).</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Artist,Admin")]
        [ProducesResponseType(typeof(ArtistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateArtistRequest request, IFormFile? avatar)
        {
            // Validar ownership: un artista solo puede editar su propio perfil
            var callerArtistId = User.FindFirst("artistId")?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && callerArtistId != id.ToString())
                return Forbid();

            string? avatarUrl = null;
            if (avatar != null)
            {
                try { avatarUrl = await _fileStorage.SaveAsync(avatar, id); }
                catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
            }

            var updated = await _artistService.UpdateAsync(id, request, avatarUrl);
            return updated is null ? NotFound() : Ok(updated);
        }
    }
}
