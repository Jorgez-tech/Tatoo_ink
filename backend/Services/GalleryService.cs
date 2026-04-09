using backend.Data;
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

        public async Task<IEnumerable<GalleryImage>> GetAllImagesAsync(bool onlyPublic = true)
        {
            try
            {
                var query = _context.GalleryImages.AsQueryable();

                if (onlyPublic)
                {
                    query = query.Where(i => i.IsPublic);
                }

                return await query
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving gallery images");
                return Enumerable.Empty<GalleryImage>();
            }
        }

        public async Task<GalleryImage?> GetImageByIdAsync(int id)
        {
            return await _context.GalleryImages.FindAsync(id);
        }

        public async Task<GalleryImage> CreateImageAsync(GalleryImageCreateDto dto)
        {
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
            return image;
        }

        public async Task<GalleryImage?> UpdateImageAsync(int id, GalleryImageUpdateDto dto)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null) return null;

            if (dto.Src != null) image.Src = dto.Src;
            if (dto.Fallback != null) image.Fallback = dto.Fallback;
            if (dto.Alt != null) image.Alt = dto.Alt;
            if (dto.Category != null) image.Category = dto.Category;
            if (dto.Photographer != null) image.Photographer = dto.Photographer;
            if (dto.Description != null) image.Description = dto.Description;
            if (dto.IsPublic.HasValue) image.IsPublic = dto.IsPublic.Value;

            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image == null) return false;

            _context.GalleryImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
