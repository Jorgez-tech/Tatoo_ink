using backend.Data;
using backend.Models;
using backend.Exceptions;
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

        public async Task<IEnumerable<GalleryImage>> GetAllImagesAsync(bool onlyPublic = true)
        {
            _logger.LogInformation("GetAllImagesAsync: Consultando galería {Type}", onlyPublic ? "pública" : "completa");
            var query = _context.GalleryImages.AsQueryable();

            if (onlyPublic)
            {
                query = query.Where(i => i.IsPublic);
            }

            return await query
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<GalleryImage> GetImageByIdAsync(int id)
        {
            _logger.LogInformation("GetImageByIdAsync: Consultando imagen ID {ImageId}", id);
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null)
            {
                _logger.LogWarning("GetImageByIdAsync: Imagen ID {ImageId} no encontrada", id);
                throw new NotFoundException($"Imagen con ID {id} no encontrada");
            }
            return image;
        }

        public async Task<GalleryImage> CreateImageAsync(GalleryImageCreateDto dto)
        {
            _logger.LogInformation("CreateImageAsync: Creando nueva imagen");
            var image = new GalleryImage
            {
                Src = dto.Src,
                Fallback = dto.Fallback,
                Alt = dto.Alt,
                Category = dto.Category,
                Photographer = dto.Photographer,
                Description = dto.Description,
                IsPublic = dto.IsPublic,
                CreatedAt = DateTime.UtcNow
            };

            _context.GalleryImages.Add(image);
            await _context.SaveChangesAsync();
            _logger.LogInformation("CreateImageAsync: Imagen creada. ID: {ImageId}", image.Id);
            return image;
        }

        public async Task<GalleryImage> UpdateImageAsync(int id, GalleryImageUpdateDto dto)
        {
            _logger.LogInformation("UpdateImageAsync: Actualizando imagen ID {ImageId}", id);
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null)
            {
                _logger.LogWarning("UpdateImageAsync: Imagen ID {ImageId} no encontrada", id);
                throw new NotFoundException($"Imagen con ID {id} no encontrada");
            }

            if (dto.Src != null) image.Src = dto.Src;
            if (dto.Fallback != null) image.Fallback = dto.Fallback;
            if (dto.Alt != null) image.Alt = dto.Alt;
            if (dto.Category != null) image.Category = dto.Category;
            if (dto.Photographer != null) image.Photographer = dto.Photographer;
            if (dto.Description != null) image.Description = dto.Description;
            if (dto.IsPublic.HasValue) image.IsPublic = dto.IsPublic.Value;

            await _context.SaveChangesAsync();
            _logger.LogInformation("UpdateImageAsync: Imagen ID {ImageId} actualizada", id);
            return image;
        }

        public async Task DeleteImageAsync(int id)
        {
            _logger.LogInformation("DeleteImageAsync: Eliminando imagen ID {ImageId}", id);
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null)
            {
                _logger.LogWarning("DeleteImageAsync: Imagen ID {ImageId} no encontrada", id);
                throw new NotFoundException($"Imagen con ID {id} no encontrada");
            }

            _context.GalleryImages.Remove(image);
            await _context.SaveChangesAsync();
            _logger.LogInformation("DeleteImageAsync: Imagen ID {ImageId} eliminada", id);
        }
    }
}
