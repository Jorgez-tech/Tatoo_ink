using System;
using System.Text;
using System.Text.Json;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace backend.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizationMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public AuthorizationMiddleware(
            RequestDelegate next,
            ILogger<AuthorizationMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrWhiteSpace(authHeader))
            {
                try
                {
                    if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        var token = authHeader.Substring("Bearer ".Length);
                        var user = ValidateAndExtractUser(token);

                        if (user != null && !user.IsExpired)
                        {
                            context.Items["User"] = user;
                            _logger.LogInformation(
                                "Token válido para usuario: {Email} (rol: {Role})",
                                user.Email,
                                user.Role);
                        }
                        else if (user != null && user.IsExpired)
                        {
                            _logger.LogWarning("Token expirado para usuario: {Email}", user.Email);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error al validar token en middleware de autorización");
                }
            }

            await _next(context);
        }

        private AuthenticatedUser? ValidateAndExtractUser(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                {
                    return null;
                }

                var payloadJson = Base64UrlDecode(parts[1]);
                using (var doc = JsonDocument.Parse(payloadJson))
                {
                    var root = doc.RootElement;

                    if (!root.TryGetProperty("sub", out var sub) ||
                        !root.TryGetProperty("email", out var email) ||
                        !root.TryGetProperty("role", out var role) ||
                        !root.TryGetProperty("exp", out var exp))
                    {
                        return null;
                    }

                    return new AuthenticatedUser
                    {
                        UserId = sub.GetInt32(),
                        Email = email.GetString() ?? string.Empty,
                        Role = role.GetString() ?? string.Empty,
                        ExpiresAtUnix = exp.GetInt64()
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        private static string Base64UrlDecode(string value)
        {
            value = value.Replace('-', '+').Replace('_', '/');
            var padLength = value.Length % 4;
            if (padLength > 0)
            {
                value += new string('=', 4 - padLength);
            }

            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}