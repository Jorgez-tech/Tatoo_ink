using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 7: Unique identifier generation
    public class ContactMessageIdPropertyTests
    {
        [Fact]
        public async Task ContactMessages_Should_Have_Unique_Ids_When_Persisted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactMessageIdTestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var messages = new List<ContactMessage>
            {
                new ContactMessage { Name = "A", Email = "a@a.com", Phone = "1", Message = "msg1", WantsAppointment = false },
                new ContactMessage { Name = "B", Email = "b@b.com", Phone = "2", Message = "msg2", WantsAppointment = true },
                new ContactMessage { Name = "C", Email = "c@c.com", Phone = "3", Message = "msg3", WantsAppointment = false }
            };

            foreach (var msg in messages)
            {
                context.ContactMessages.Add(msg);
            }
            await context.SaveChangesAsync();

            var ids = context.ContactMessages.Select(m => m.Id).ToList();
            Assert.Equal(ids.Count, ids.Distinct().Count());
            Assert.All(ids, id => Assert.True(id > 0));
        }
    }
}
