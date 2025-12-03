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

        public async Task<IEnumerable<GalleryImage>> GetAllImagesAsync()
        {
            try
            {
                return await _context.GalleryImages
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving gallery images");
                return Enumerable.Empty<GalleryImage>();
            }
        }
    }
}
