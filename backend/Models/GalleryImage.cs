using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Src { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Alt { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
