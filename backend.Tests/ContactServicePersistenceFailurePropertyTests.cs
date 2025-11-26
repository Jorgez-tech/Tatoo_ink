using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 9: Persistence failure handling
    public class ContactServicePersistenceFailurePropertyTests
    {
        [Fact]
        public async Task ContactService_Should_Not_Send_Email_If_Persistence_Fails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServicePersistenceFailureTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);

            // Simular fallo de persistencia
            var contextMock = new Mock<ApplicationDbContext>(options);
            contextMock.Setup(x => x.SaveChangesAsync(default)).ThrowsAsync(new DbUpdateException("Simulated DB failure"));

            var emailServiceMock = new Mock<IEmailService>();
            bool emailCalled = false;
            emailServiceMock.Setup(x => x.SendContactNotificationAsync(It.IsAny<ContactMessage>()))
                .Callback(() => emailCalled = true)
                .ReturnsAsync(true);
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();

            var service = new ContactService(contextMock.Object, emailServiceMock.Object, loggerMock.Object);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@test.com",
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.False(result.Success);
            Assert.False(emailCalled);
        }
    }
}
