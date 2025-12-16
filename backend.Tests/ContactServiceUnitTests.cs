using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System;

namespace backend.Tests
{
    public class ContactServiceUnitTests
    {
        [Fact]
        public async Task ProcessContactMessageAsync_Should_Map_Dto_To_Entity_Correctly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceMapDtoTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);
            var emailService = new RecordingEmailService(result: true);
            var logger = new TestLogger<ContactService>();
            var service = new ContactService(context, emailService, logger);
            var dto = new ContactRequestDto
            {
                Name = "TestName",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            var entity = await context.ContactMessages.FindAsync(result.Id);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Email, entity.Email);
            Assert.Equal(dto.Phone, entity.Phone);
            Assert.Equal(dto.Message, entity.Message);
            Assert.Equal(dto.WantsAppointment, entity.WantsAppointment);
        }

        [Fact]
        public async Task ProcessContactMessageAsync_Should_Handle_DbException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceDbExceptionTestDb")
                .Options;
            using var context = new ThrowingApplicationDbContext(options, new DbUpdateException("Simulated DB error"));
            var emailService = new RecordingEmailService(result: true);
            var logger = new TestLogger<ContactService>();
            var service = new ContactService(context, emailService, logger);
            var dto = new ContactRequestDto
            {
                Name = "TestName",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.False(result.Success);
            Assert.True(result.Error.Contains("Error al guardar el mensaje") || result.Error.Contains("Ocurri� un error inesperado"));
        }

        [Fact]
        public async Task ProcessContactMessageAsync_Should_Handle_EmailException_Gracefully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceEmailExceptionTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);
            var emailService = new ThrowingEmailService(new Exception("Simulated email error"));
            var logger = new TestLogger<ContactService>();
            var service = new ContactService(context, emailService, logger);
            var dto = new ContactRequestDto
            {
                Name = "TestName",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await service.ProcessContactMessageAsync(dto);
            Assert.True(result.Success);
            Assert.NotNull(result.Id);
        }
    }
}
