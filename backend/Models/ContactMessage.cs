using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
        [Required]
        public bool WantsAppointment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool EmailSent { get; set; } = false;
        public DateTime? EmailSentAt { get; set; }
    }

    public class ContactRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
        [Required]
        public bool WantsAppointment { get; set; }
    }

    public class ContactResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? Id { get; set; }
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Error { get; set; } = string.Empty;
        public int? Id { get; set; }
    }
}
