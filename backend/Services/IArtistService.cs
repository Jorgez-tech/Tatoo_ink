using backend.DTOs;

namespace backend.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistResponse>> GetAllAsync();
        Task<ArtistResponse?> GetByIdAsync(int id);
        Task<ArtistResponse?> GetByUserIdAsync(string userId);
        Task<ArtistResponse?> UpdateAsync(int id, UpdateArtistRequest request, string? avatarUrl);
    }
}
