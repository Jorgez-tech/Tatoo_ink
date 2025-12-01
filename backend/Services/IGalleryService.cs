using backend.Models;

namespace backend.Services
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryImage>> GetAllImagesAsync();
    }
}
