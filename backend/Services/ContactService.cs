using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactService> _logger;

        public ContactService(
            ApplicationDbContext context,
            IEmailService emailService,
            ILogger<ContactService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<ServiceResult> ProcessContactMessageAsync(ContactRequestDto request)
        {
            try
            {
                // Mapear DTO a entidad
                var contactMessage = new ContactMessage
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Message = request.Message,
                    WantsAppointment = request.WantsAppointment,
                    CreatedAt = DateTime.UtcNow,
                    EmailSent = false
                };

                // Persistir en base de datos
                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Mensaje de contacto guardado exitosamente. ID: {ContactId}", contactMessage.Id);

                // Intentar enviar email
                try
                {
                    var emailSent = await _emailService.SendContactNotificationAsync(contactMessage);
                    
                    if (emailSent)
                    {
                        // Actualizar estado de email enviado
                        contactMessage.EmailSent = true;
                        contactMessage.EmailSentAt = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                        
                        _logger.LogInformation("Email enviado exitosamente para el contacto {ContactId}", contactMessage.Id);
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo enviar el email para el contacto {ContactId}, pero el mensaje fue guardado", contactMessage.Id);
                    }
                }
                catch (Exception emailEx)
                {
                    // Registrar error de email pero retornar éxito (mensaje ya guardado)
                    _logger.LogError(emailEx, "Error al enviar email para el contacto {ContactId}, pero el mensaje fue guardado", contactMessage.Id);
                }

                return new ServiceResult
                {
                    Success = true,
                    Id = contactMessage.Id
                };
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al guardar mensaje de contacto");
                return new ServiceResult
                {
                    Success = false,
                    Error = "Error al guardar el mensaje. Por favor, intente nuevamente."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al procesar mensaje de contacto");
                return new ServiceResult
                {
                    Success = false,
                    Error = "Ocurrió un error inesperado. Por favor, intente nuevamente."
                };
            }
        }
    }
}
