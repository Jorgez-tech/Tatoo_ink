using backend.Models;
using backend.Services;
using Xunit;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 10: Complete email content
    public class EmailServiceContentPropertyTests
    {
        [Theory]
        [InlineData("Juan", "juan@email.com", "123456789", "Mensaje de prueba", true)]
        [InlineData("Ana", "ana@email.com", "987654321", "Consulta por cita", false)]
        public void GenerateEmailTemplate_Should_Include_All_Fields(string name, string email, string phone, string message, bool wantsAppointment)
        {
            var contact = new ContactMessage
            {
                Name = name,
                Email = email,
                Phone = phone,
                Message = message,
                WantsAppointment = wantsAppointment,
                CreatedAt = System.DateTime.UtcNow
            };
            var sendGridService = new SendGridEmailService(null, null);
            var smtpService = new SmtpEmailService(null, null);
            var htmlSendGrid = sendGridService.GetType().GetMethod("GenerateEmailTemplate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(sendGridService, new object[] { contact }) as string;
            var htmlSmtp = smtpService.GetType().GetMethod("GenerateEmailTemplate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(smtpService, new object[] { contact }) as string;
            Assert.Contains(name, htmlSendGrid);
            Assert.Contains(email, htmlSendGrid);
            Assert.Contains(phone, htmlSendGrid);
            Assert.Contains(message, htmlSendGrid);
            Assert.Contains(wantsAppointment ? "Solicita Cita" : "Sin cita", htmlSendGrid);
            Assert.Contains(name, htmlSmtp);
            Assert.Contains(email, htmlSmtp);
            Assert.Contains(phone, htmlSmtp);
            Assert.Contains(message, htmlSmtp);
            Assert.Contains(wantsAppointment ? "Solicita Cita" : "Sin cita", htmlSmtp);
        }
    }
}
