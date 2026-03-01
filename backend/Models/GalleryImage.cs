using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        /// <summary>Ruta relativa de la imagen, ej: /uploads/1/foto.jpg</summary>
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

        /// <summary>Descripción del trabajo escrita por el artista</summary>
        [MaxLength(2000)]
        public string? Description { get; set; }

        // FK al artista propietario de esta imagen
        public int? ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist? Artist { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
