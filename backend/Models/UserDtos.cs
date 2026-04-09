using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class UserCreateDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(128)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("admin|artist", ErrorMessage = "El rol debe ser 'admin' o 'artist'")]
        public string Role { get; set; } = "artist";

        public bool IsActive { get; set; } = true;
    }

    public class UserUpdateDto
    {
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MinLength(8)]
        [MaxLength(128)]
        public string? Password { get; set; }

        [RegularExpression("admin|artist", ErrorMessage = "El rol debe ser 'admin' o 'artist'")]
        public string? Role { get; set; }

        public bool? IsActive { get; set; }
    }

    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
