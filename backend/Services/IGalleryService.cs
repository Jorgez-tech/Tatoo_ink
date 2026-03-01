using backend.DTOs;
using backend.Models;

namespace backend.Services
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryImageResponse>> GetAllImagesAsync();
        Task<IEnumerable<GalleryImageResponse>> GetByArtistAsync(int artistId);
        Task<GalleryImageResponse?> GetByIdAsync(int id);
        Task<GalleryImageResponse> CreateAsync(CreateGalleryImageRequest request, string imagePath, int? artistId);
        Task<GalleryImageResponse?> UpdateAsync(int id, UpdateGalleryImageRequest request);
        Task<bool> DeleteAsync(int id, int? callerArtistId, bool isAdmin);
        Task<GalleryImage?> GetEntityByIdAsync(int id);
    }
}
