using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class BusinessSettings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string BusinessName { get; set; } = string.Empty;

        [Required]
        [MaxLength(180)]
        public string BusinessTagline { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string BusinessDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? InstagramUrl { get; set; }

        [MaxLength(300)]
        public string? FacebookUrl { get; set; }

        [MaxLength(300)]
        public string? TwitterUrl { get; set; }

        [Required]
        [MaxLength(500)]
        public string Schedule { get; set; } = "Lun - Sáb: 10:00 - 20:00";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int? UpdatedByUserId { get; set; }
    }
}
