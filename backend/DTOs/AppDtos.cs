namespace backend.DTOs
{
    // ── Auth ──────────────────────────────────────────────────────────────────

    public record LoginRequest(string Email, string Password);

    public record LoginResponse(
        string Token,
        string RefreshToken,
        string Email,
        string Role,
        int? ArtistId,
        string? DisplayName
    );

    public record RefreshTokenRequest(string RefreshToken);

    // ── Artist ────────────────────────────────────────────────────────────────

    public record ArtistResponse(
        int Id,
        string DisplayName,
        string? Bio,
        string? InstagramUrl,
        string? AvatarUrl
    );

    public record UpdateArtistRequest(
        string? DisplayName,
        string? Bio,
        string? InstagramUrl
    );

    // ── Gallery ───────────────────────────────────────────────────────────────

    public class GalleryImageResponse
    {
        public int Id { get; set; }
        public string Src { get; set; } = string.Empty;
        public string? Fallback { get; set; }
        public string Alt { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int? ArtistId { get; set; }
        public string? ArtistName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateGalleryImageRequest
    {
        public string Alt { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateGalleryImageRequest
    {
        public string? Alt { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
