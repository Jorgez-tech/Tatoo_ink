using System;

namespace backend.Models
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public long ExpiresAtUnix { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= ExpiresAtUnix;
    }
}