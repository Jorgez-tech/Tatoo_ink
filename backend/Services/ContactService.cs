using backend.Data;
using backend.Models;
using backend.Exceptions;
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

        public async Task<ContactMessage> ProcessContactMessageAsync(ContactRequestDto request)
        {
            _logger.LogInformation("ProcessContactMessageAsync: Procesando mensaje de {Email}", request.Email);

            // Mapear DTO a entidad
            var contactMessage = new ContactMessage
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Message = request.Message,
                WantsAppointment = request.WantsAppointment ?? false,
                CreatedAt = DateTime.UtcNow,
                EmailSent = false
            };

            // Persistir en base de datos - dejar que lance excepción si falla
            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            _logger.LogInformation("ProcessContactMessageAsync: Mensaje guardado. ID: {ContactId}", contactMessage.Id);

            // Intentar enviar email de forma asincrónica (fire-and-forget)
            // Si falla, el mensaje ya está guardado en la BD
            _ = SendEmailAsync(contactMessage);

            return contactMessage;
        }

        private async Task SendEmailAsync(ContactMessage contactMessage)
        {
            try
            {
                var emailSent = await _emailService.SendContactNotificationAsync(contactMessage);
                
                if (emailSent)
                {
                    contactMessage.EmailSent = true;
                    contactMessage.EmailSentAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("SendEmailAsync: Email enviado para contacto {ContactId}", contactMessage.Id);
                }
                else
                {
                    _logger.LogWarning("SendEmailAsync: No se pudo enviar email para contacto {ContactId}", contactMessage.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmailAsync: Error al enviar email para contacto {ContactId}", contactMessage.Id);
                // No relanzar excepción - el mensaje ya está guardado
            }
        }
        public async Task<IEnumerable<ContactMessageDto>> GetAllMessagesAsync()
        {
            _logger.LogInformation("GetAllMessagesAsync: Consultando todos los mensajes de contacto");
            return await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new ContactMessageDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Phone = m.Phone,
                    Message = m.Message,
                    WantsAppointment = m.WantsAppointment,
                    CreatedAt = m.CreatedAt,
                    EmailSent = m.EmailSent
                })
                .ToListAsync();
        }

        public async Task<ContactMessageDto> GetMessageByIdAsync(int id)
        {
            _logger.LogInformation("GetMessageByIdAsync: Consultando mensaje ID {MessageId}", id);
            var m = await _context.ContactMessages.FindAsync(id);
            if (m == null)
            {
                _logger.LogWarning("GetMessageByIdAsync: Mensaje ID {MessageId} no encontrado", id);
                throw new NotFoundException($"Mensaje de contacto con ID {id} no encontrado");
            }

            return new ContactMessageDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Message = m.Message,
                WantsAppointment = m.WantsAppointment,
                CreatedAt = m.CreatedAt,
                EmailSent = m.EmailSent
            };
        }
    }
}
