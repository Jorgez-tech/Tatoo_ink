using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
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
            using var context = new ThrowingApplicationDbContext(options, new DbUpdateException("Simulated DB failure"));

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
            Assert.False(result.Success);
            Assert.Empty(emailService.SentMessages);
        }
    }
}
