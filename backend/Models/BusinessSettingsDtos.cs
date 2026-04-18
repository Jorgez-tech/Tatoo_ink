using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class BusinessSettingsResponseDto
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string BusinessTagline { get; set; } = string.Empty;
        public string BusinessDescription { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string Schedule { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }

    public class BusinessSettingsUpdateDto
    {
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
        [EmailAddress]
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
        public string Schedule { get; set; } = string.Empty;
    }
}
