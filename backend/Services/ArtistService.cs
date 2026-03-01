using backend.Data;
using backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistService> _logger;

        public ArtistService(ApplicationDbContext context, ILogger<ArtistService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ArtistResponse>> GetAllAsync()
        {
            return await _context.Artists
                .OrderBy(a => a.DisplayName)
                .Select(a => new ArtistResponse(a.Id, a.DisplayName, a.Bio, a.InstagramUrl, a.AvatarUrl))
                .ToListAsync();
        }

        public async Task<ArtistResponse?> GetByIdAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return null;
            return new ArtistResponse(artist.Id, artist.DisplayName, artist.Bio, artist.InstagramUrl, artist.AvatarUrl);
        }

        public async Task<ArtistResponse?> GetByUserIdAsync(string userId)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.UserId == userId);
            if (artist == null) return null;
            return new ArtistResponse(artist.Id, artist.DisplayName, artist.Bio, artist.InstagramUrl, artist.AvatarUrl);
        }

        public async Task<ArtistResponse?> UpdateAsync(int id, UpdateArtistRequest request, string? avatarUrl)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return null;

            if (request.DisplayName != null) artist.DisplayName = request.DisplayName;
            if (request.Bio != null)         artist.Bio = request.Bio;
            if (request.InstagramUrl != null) artist.InstagramUrl = request.InstagramUrl;
            if (avatarUrl != null)           artist.AvatarUrl = avatarUrl;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Artist {Id} updated", id);

            return new ArtistResponse(artist.Id, artist.DisplayName, artist.Bio, artist.InstagramUrl, artist.AvatarUrl);
        }
    }
}
