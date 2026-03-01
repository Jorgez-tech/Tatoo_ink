using System;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 8: Timestamp recording
    public class ContactMessageTimestampPropertyTests
    {
        [Fact]
        public async Task ContactMessage_Should_Have_Valid_CreatedAt_Timestamp_When_Persisted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactMessageTimestampTestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var now = DateTime.UtcNow;
            var msg = new ContactMessage
            {
                Name = "Test",
                Email = "test@test.com",
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };

            context.ContactMessages.Add(msg);
            await context.SaveChangesAsync();

            var persisted = await context.ContactMessages.FirstAsync();
            Assert.True(persisted.CreatedAt >= now);
            Assert.True((persisted.CreatedAt - now).TotalMinutes < 1);
        }
    }
}
