using backend.Models;

namespace backend.Services
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryImage>> GetAllImagesAsync(bool onlyPublic = true);
        Task<GalleryImage?> GetImageByIdAsync(int id);
        Task<GalleryImage> CreateImageAsync(GalleryImageCreateDto dto);
        Task<GalleryImage?> UpdateImageAsync(int id, GalleryImageUpdateDto dto);
        Task<bool> DeleteImageAsync(int id);
    }
}
