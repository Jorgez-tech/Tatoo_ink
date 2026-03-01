using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 12: Success response format
    public class ContactServiceSuccessResponseFormatPropertyTests
    {
        [Fact]
        public async Task ContactService_Should_Return_Success_Response_On_ValidProcessing()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceSuccessResponseFormatTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);

            var emailService = new RecordingEmailService(result: true);
            var logger = new TestLogger<ContactService>();

            var service = new ContactService(context, emailService, logger);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@test.com",
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.True(result.Success);
            Assert.NotNull(result.Id);
            Assert.True(result.Id > 0);
        }
    }
}
