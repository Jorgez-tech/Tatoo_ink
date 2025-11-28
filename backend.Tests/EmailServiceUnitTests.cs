using backend.Models;
using backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class EmailServiceUnitTests
    {
        [Fact]
        public async Task SendGridEmailService_Should_Return_False_On_Missing_ApiKey()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["EmailSettings:SendGridApiKey"]).Returns((string)null);
            var loggerMock = new Mock<ILogger<SendGridEmailService>>();
            var service = new SendGridEmailService(configMock.Object, loggerMock.Object);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var result = await service.SendContactNotificationAsync(contact);
            Assert.False(result);
        }

        [Fact]
        public async Task SmtpEmailService_Should_Return_False_On_Missing_SmtpServer()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["EmailSettings:SmtpServer"]).Returns((string)null);
            var loggerMock = new Mock<ILogger<SmtpEmailService>>();
            var service = new SmtpEmailService(configMock.Object, loggerMock.Object);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var result = await service.SendContactNotificationAsync(contact);
            Assert.False(result);
        }

        [Fact]
        public void SmtpEmailService_Should_Set_Destinatario_Correctly()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["EmailSettings:StudioEmail"]).Returns("studio@correo.com");
            configMock.Setup(x => x["EmailSettings:StudioName"]).Returns("Studio Ink");
            var loggerMock = new Mock<ILogger<SmtpEmailService>>();
            var service = new SmtpEmailService(configMock.Object, loggerMock.Object);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var html = service.GetType().GetMethod("GenerateEmailTemplate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { contact }) as string;
            Assert.Contains("Test", html);
            Assert.Contains("test@test.com", html);
            Assert.Contains("Mensaje", html);
        }
    }
}
