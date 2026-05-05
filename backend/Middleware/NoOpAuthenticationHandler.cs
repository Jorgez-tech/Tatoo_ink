using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace backend.Middleware
{
    /// <summary>
    /// Handler de autenticación sin operación (NoOp).
    /// 
    /// El proyecto usa un middleware custom (AuthorizationMiddleware) que valida el JWT
    /// manualmente con HMACSHA256 y ya inyecta los Claims en context.User ANTES de que
    /// el pipeline de ASP.NET Core llegue a UseAuthentication().
    /// 
    /// Este handler existe únicamente para que [Authorize(Roles = "admin")] funcione,
    /// ya que ASP.NET Core requiere un esquema de autenticación registrado.
    /// Su único trabajo es leer los claims ya existentes en el contexto.
    /// </summary>
    public class NoOpAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public NoOpAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Si el middleware custom ya autenticó al usuario, context.User tiene identidades.
            if (Context.User.Identity?.IsAuthenticated == true)
            {
                var ticket = new AuthenticationTicket(Context.User, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            // No hay token válido — retornamos NoResult (no Fail) para que otros
            // middlewares o [AllowAnonymous] puedan manejar el caso correctamente.
            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
