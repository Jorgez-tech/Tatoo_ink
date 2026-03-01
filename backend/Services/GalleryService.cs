using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GalleryService> _logger;

        public GalleryService(ApplicationDbContext context, ILogger<GalleryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GalleryImageResponse>> GetAllImagesAsync()
        {
            try
            {
                // Materializar primero con ToList() antes de llamar a MapToResponse.
                // EF Core no puede traducir llamadas a métodos estáticos .NET en IQueryable.Select.
                var images = await _context.GalleryImages
                    .Include(g => g.Artist)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();

                return images.Select(MapToResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving gallery images");
                return Enumerable.Empty<GalleryImageResponse>();
            }
        }

        public async Task<IEnumerable<GalleryImageResponse>> GetByArtistAsync(int artistId)
        {
            // Mismo patrón: materializar primero, luego proyectar en memoria.
            var images = await _context.GalleryImages
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == artistId)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();

            return images.Select(MapToResponse);
        }

        public async Task<GalleryImageResponse?> GetByIdAsync(int id)
        {
            var image = await _context.GalleryImages
                .Include(g => g.Artist)
                .FirstOrDefaultAsync(g => g.Id == id);

            return image == null ? null : MapToResponse(image);
        }

        public async Task<GalleryImage?> GetEntityByIdAsync(int id)
        {
            return await _context.GalleryImages.FindAsync(id);
        }

        public async Task<GalleryImageResponse> CreateAsync(CreateGalleryImageRequest request, string imagePath, int? artistId)
        {
            var image = new GalleryImage
            {
                Src         = imagePath,
                Alt         = request.Alt,
                Category    = request.Category,
                Description = request.Description,
                ArtistId    = artistId,
                CreatedAt   = DateTime.UtcNow
            };

            _context.GalleryImages.Add(image);
            await _context.SaveChangesAsync();

            // Recargar con relación para el response
            await _context.Entry(image).Reference(g => g.Artist).LoadAsync();

            _logger.LogInformation("Gallery image {Id} created by artist {ArtistId}", image.Id, artistId);
            return MapToResponse(image);
        }

        public async Task<GalleryImageResponse?> UpdateAsync(int id, UpdateGalleryImageRequest request)
        {
            var image = await _context.GalleryImages
                .Include(g => g.Artist)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (image == null) return null;

            if (request.Alt         != null) image.Alt         = request.Alt;
            if (request.Category    != null) image.Category    = request.Category;
            if (request.Description != null) image.Description = request.Description;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Gallery image {Id} updated", id);
            return MapToResponse(image);
        }

        /// <summary>
        /// Elimina una imagen. Valida que el caller sea el propietario o un Admin.
        /// Retorna false si no existe o no tiene permisos.
        /// </summary>
        public async Task<bool> DeleteAsync(int id, int? callerArtistId, bool isAdmin)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null) return false;

            // Validar ownership (los Admin pueden borrar cualquier imagen)
            if (!isAdmin && image.ArtistId != callerArtistId)
            {
                _logger.LogWarning("Artist {ArtistId} attempted to delete image {ImageId} owned by artist {OwnerArtistId}",
                    callerArtistId, id, image.ArtistId);
                return false;
            }

            _context.GalleryImages.Remove(image);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Gallery image {Id} deleted", id);
            return true;
        }

        private static GalleryImageResponse MapToResponse(GalleryImage g) =>
            new GalleryImageResponse
            {
                Id          = g.Id,
                Src         = g.Src,
                Fallback    = g.Fallback,
                Alt         = g.Alt,
                Category    = g.Category,
                Description = g.Description,
                ArtistId    = g.ArtistId,
                ArtistName  = g.Artist?.DisplayName,
                CreatedAt   = g.CreatedAt
            };
    }
}
