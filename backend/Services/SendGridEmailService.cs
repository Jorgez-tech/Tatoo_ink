using backend.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace backend.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendGridEmailService> _logger;

        public SendGridEmailService(IConfiguration configuration, ILogger<SendGridEmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendContactNotificationAsync(ContactMessage message)
        {
            try
            {
                var apiKey = _configuration["EmailSettings:SendGridApiKey"];
                var studioEmail = _configuration["EmailSettings:StudioEmail"];
                var studioName = _configuration["EmailSettings:StudioName"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogError("SendGrid API Key no está configurada");
                    return false;
                }

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(studioEmail, studioName);
                var to = new EmailAddress(studioEmail, studioName);
                var subject = $"Nuevo mensaje de contacto - {message.Name}";
                
                var htmlContent = GenerateEmailTemplate(message);
                
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email enviado exitosamente para el contacto {ContactId}", message.Id);
                    return true;
                }
                else
                {
                    _logger.LogError("Error al enviar email. Status: {StatusCode}", response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al enviar email para el contacto {ContactId}", message.Id);
                return false;
            }
        }

        private string GenerateEmailTemplate(ContactMessage message)
        {
            var appointmentBadge = message.WantsAppointment 
                ? "<span style='background-color: #10b981; color: white; padding: 4px 12px; border-radius: 4px; font-size: 14px;'>✓ Solicita Cita</span>"
                : "<span style='background-color: #6b7280; color: white; padding: 4px 12px; border-radius: 4px; font-size: 14px;'>Sin cita</span>";

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #1f2937; color: white; padding: 20px; text-align: center; }}
        .content {{ background-color: #f9fafb; padding: 20px; }}
        .field {{ margin-bottom: 15px; }}
        .label {{ font-weight: bold; color: #4b5563; }}
        .value {{ color: #1f2937; }}
        .footer {{ text-align: center; padding: 20px; color: #6b7280; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Nuevo Mensaje de Contacto</h1>
        </div>
        <div class='content'>
            <div style='margin-bottom: 20px;'>
                {appointmentBadge}
            </div>
            
            <div class='field'>
                <div class='label'>Nombre:</div>
                <div class='value'>{message.Name}</div>
            </div>
            
            <div class='field'>
                <div class='label'>Email:</div>
                <div class='value'>{message.Email}</div>
            </div>
            
            <div class='field'>
                <div class='label'>Teléfono:</div>
                <div class='value'>{message.Phone}</div>
            </div>
            
            <div class='field'>
                <div class='label'>Mensaje:</div>
                <div class='value'>{message.Message}</div>
            </div>
            
            <div class='field'>
                <div class='label'>Fecha:</div>
                <div class='value'>{message.CreatedAt:dd/MM/yyyy HH:mm}</div>
            </div>
        </div>
        <div class='footer'>
            Este mensaje fue enviado desde el formulario de contacto de tu sitio web.
        </div>
    </div>
</body>
</html>";
        }
    }
}
