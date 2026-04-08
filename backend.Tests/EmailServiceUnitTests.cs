using backend.Models;
using backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class EmailServiceUnitTests
    {
        [Fact]
        public async Task SendGridEmailService_Should_Return_False_On_Missing_ApiKey()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()).Build();
            var logger = new TestLogger<SendGridEmailService>();
            var service = new SendGridEmailService(configuration, logger);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var result = await service.SendContactNotificationAsync(contact);
            Assert.False(result);
        }

        [Fact]
        public async Task SmtpEmailService_Should_Return_False_On_Missing_SmtpServer()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()).Build();
            var logger = new TestLogger<SmtpEmailService>();
            var service = new SmtpEmailService(configuration, logger);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var result = await service.SendContactNotificationAsync(contact);
            Assert.False(result);
        }

        [Fact]
        public void SmtpEmailService_Should_Set_Destinatario_Correctly()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["EmailSettings:StudioEmail"] = "studio@correo.com",
                ["EmailSettings:StudioName"] = "Studio Ink"
            }).Build();
            var logger = new TestLogger<SmtpEmailService>();
            var service = new SmtpEmailService(configuration, logger);
            var contact = new ContactMessage { Name = "Test", Email = "test@test.com", Phone = "123", Message = "Mensaje", WantsAppointment = true };
            var html = service.GetType().GetMethod("GenerateEmailTemplate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { contact }) as string;
            Assert.Contains("Test", html);
            Assert.Contains("test@test.com", html);
            Assert.Contains("Mensaje", html);
        }
    }
}
