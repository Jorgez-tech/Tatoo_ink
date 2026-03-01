using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    /// <summary>
    /// Usuario de la aplicación. Extiende IdentityUser para habilitar
    /// autenticación con ASP.NET Core Identity.
    /// La relación con Artist es 1-a-1 opcional: los admins no tienen artista.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public Artist? Artist { get; set; }
    }
}
