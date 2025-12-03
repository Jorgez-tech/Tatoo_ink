using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 4: Appointment flag preservation
    public class ContactServiceAppointmentFlagPropertyTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ContactService_Should_Preserve_WantsAppointment_Flag(bool wantsAppointment)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceAppointmentFlagTestDb" + wantsAppointment)
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
            var persisted = await context.ContactMessages.FirstAsync();
            Assert.Equal(wantsAppointment, persisted.WantsAppointment);
        }
    }
}
