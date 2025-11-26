using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 5: Processing independence from appointment flag
    public class ContactServiceIndependencePropertyTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ContactService_Should_Process_Both_Flags_Identically(bool wantsAppointment)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceIndependenceTestDb" + wantsAppointment)
                .Options;
            using var context = new ApplicationDbContext(options);

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.SendContactNotificationAsync(It.IsAny<ContactMessage>())).ReturnsAsync(true);
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();

            var service = new ContactService(context, emailServiceMock.Object, loggerMock.Object);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@test.com",
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = wantsAppointment
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.True(result.Success);
            Assert.NotNull(result.Id);
            var persisted = await context.ContactMessages.FindAsync(result.Id);
            Assert.Equal(wantsAppointment, persisted.WantsAppointment);
        }
    }
}
