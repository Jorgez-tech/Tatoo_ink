using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace backend.Utils
{
    public static class ConfigurationValidator
    {
        public static void Validate(IConfiguration config)
        {
            var environment = config["ASPNETCORE_ENVIRONMENT"] ?? "Production";
            var isDevelopment = environment == "Development";

            // Validar cadena de conexion
            var conn = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(conn))
                throw new Exception("Falta la cadena de conexion a la base de datos.");

            // Validar credenciales de email
            var provider = config["EmailSettings:Provider"];
            if (provider?.ToLower() == "sendgrid")
            {
                var apiKey = config["EmailSettings:SendGridApiKey"];
                // Solo requerir API Key si no es localhost/desarrollo
                if (string.IsNullOrWhiteSpace(apiKey) && !isDevelopment)
                    throw new Exception("Falta la API Key de SendGrid para produccion.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpServer"]))
                    throw new Exception("Falta el servidor SMTP.");
                
                // Solo requerir autenticacion en produccion y si no es localhost
                var smtpServer = config["EmailSettings:SmtpServer"];
                var isLocalhost = smtpServer?.Contains("localhost") == true || smtpServer?.Contains("127.0.0.1") == true;
                
                if (!isDevelopment && !isLocalhost)
                {
                    if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpUsername"]))
                        throw new Exception("Falta el usuario SMTP.");
                    if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpPassword"]))
                        throw new Exception("Falta la contrasena SMTP.");
                }
            }
            if (string.IsNullOrWhiteSpace(config["EmailSettings:StudioEmail"]))
                throw new Exception("Falta el email del estudio.");

            // Validar configuracion de CORS
            var cors = config.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
            if (cors == null || !cors.Any())
                throw new Exception("Falta la configuracion de CORS (AllowedOrigins).");
        }
    }
}