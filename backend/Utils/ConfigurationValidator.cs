using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace backend.Utils
{
    public static class ConfigurationValidator
    {
        public static void Validate(IConfiguration config)
        {
            // Validar cadena de conexión
            var conn = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(conn))
                throw new Exception("Falta la cadena de conexión a la base de datos.");

            // Validar credenciales de email
            var provider = config["EmailSettings:Provider"];
            if (provider?.ToLower() == "sendgrid")
            {
                if (string.IsNullOrWhiteSpace(config["EmailSettings:SendGridApiKey"]))
                    throw new Exception("Falta la API Key de SendGrid.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpServer"]))
                    throw new Exception("Falta el servidor SMTP.");
                if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpUsername"]))
                    throw new Exception("Falta el usuario SMTP.");
                if (string.IsNullOrWhiteSpace(config["EmailSettings:SmtpPassword"]))
                    throw new Exception("Falta la contraseña SMTP.");
            }
            if (string.IsNullOrWhiteSpace(config["EmailSettings:StudioEmail"]))
                throw new Exception("Falta el email del estudio.");

            // Validar configuración de CORS
            var cors = config.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
            if (cors == null || !cors.Any())
                throw new Exception("Falta la configuración de CORS (AllowedOrigins).");
        }
    }
}
