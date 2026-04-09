using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class GalleryImageCreateDto
    {
        [Required]
        [MaxLength(500)]
        public string Src { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Fallback { get; set; }

        [Required]
        [MaxLength(200)]
        public string Alt { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(100)]
        public string? Photographer { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsPublic { get; set; } = true;
    }

    public class GalleryImageUpdateDto
    {
        [MaxLength(500)]
        public string? Src { get; set; }

        [MaxLength(500)]
        public string? Fallback { get; set; }

        [MaxLength(200)]
        public string? Alt { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(100)]
        public string? Photographer { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool? IsPublic { get; set; }
    }
}
