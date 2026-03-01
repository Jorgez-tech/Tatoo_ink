using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// Representa un artista del estudio. Tiene una relación 1-a-1 con ApplicationUser
    /// y una relación 1-a-N con GalleryImage (sus trabajos).
    /// </summary>
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Bio { get; set; }

        [MaxLength(200)]
        public string? InstagramUrl { get; set; }

        [MaxLength(500)]
        public string? AvatarUrl { get; set; }

        // FK hacia ApplicationUser (Identity)
        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        // Trabajos del artista
        public ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
