using backend.Models;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
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
            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.SendContactNotificationAsync(It.IsAny<ContactMessage>())).ReturnsAsync(true);
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();
            var service = new ContactService(context, emailServiceMock.Object, loggerMock.Object);
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
            var contextMock = new Mock<ApplicationDbContext>(options);
            contextMock.Setup(x => x.SaveChangesAsync(default)).ThrowsAsync(new DbUpdateException("Simulated DB error"));
            var emailServiceMock = new Mock<IEmailService>();
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();
            var service = new ContactService(contextMock.Object, emailServiceMock.Object, loggerMock.Object);
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
            Assert.True(result.Error.Contains("Error al guardar el mensaje") || result.Error.Contains("Ocurrió un error inesperado"));
        }

        [Fact]
        public async Task ProcessContactMessageAsync_Should_Handle_EmailException_Gracefully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactServiceEmailExceptionTestDb")
                .Options;
            using var context = new ApplicationDbContext(options);
            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.SendContactNotificationAsync(It.IsAny<ContactMessage>())).ThrowsAsync(new Exception("Simulated email error"));
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<ContactService>>();
            var service = new ContactService(context, emailServiceMock.Object, loggerMock.Object);
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
