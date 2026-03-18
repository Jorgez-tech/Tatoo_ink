using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Token { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
    }
}