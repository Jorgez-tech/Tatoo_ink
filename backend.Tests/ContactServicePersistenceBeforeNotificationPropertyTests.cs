using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 6: Persistence before notification
    public class ContactServicePersistenceBeforeNotificationPropertyTests
    {
        [Fact]
        public async Task ContactService_Should_Persist_Before_EmailNotification()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServicePersistenceBeforeNotificationTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);

            var emailServiceMock = new Mock<IEmailService>();
            bool emailCalled = false;
            emailServiceMock.Setup(x => x.SendContactNotificationAsync(It.IsAny<ContactMessage>()))
                .Callback<ContactMessage>(msg => {
                    // Verificar que el mensaje ya está en la base de datos
                    var exists = context.ContactMessages.ToList().Any(m => m.Id == msg.Id);
                    Assert.True(exists);
                    emailCalled = true;
                })
                .ReturnsAsync(true);
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();

            var service = new ContactService(context, emailServiceMock.Object, loggerMock.Object);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@test.com",
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.True(emailCalled);
        }
    }
}
