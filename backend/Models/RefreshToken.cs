namespace backend.Models
{
    /// <summary>
    /// Represent a refresh token persisted en base de datos.
    /// Solo se almacena el hash SHA-256 del token real para evitar exposición en caso de leak de DB.
    /// </summary>
    public class RefreshToken
    {
        public int Id { get; set; }

        /// <summary>Hash SHA-256 (hex) del token real enviado al cliente.</summary>
        public string TokenHash { get; set; } = string.Empty;

        /// <summary>FK al usuario propietario del token.</summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>Fecha/hora de expiración (UTC).</summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>Indica si el token fue revocado explícitamente (logout o rotación).</summary>
        public bool IsRevoked { get; set; }

        /// <summary>Fecha/hora de creación (UTC).</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser User { get; set; } = null!;
    }
}
