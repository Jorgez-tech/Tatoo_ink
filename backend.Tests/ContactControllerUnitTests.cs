using backend.Controllers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using System;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class ContactControllerUnitTests
    {
        [Fact]
        public async Task Post_Should_Return_200_On_Valid_Input()
        {
            var message = new ContactMessage
            {
                Id = 1,
                Name = "Test",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true,
                CreatedAt = DateTime.UtcNow,
                EmailSent = false
            };
            var service = new FixedResultContactService(_ => Task.FromResult(message));
            var logger = new TestLogger<ContactController>();
            var controller = new ContactController(service, logger);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await controller.Post(dto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ContactResponseDto>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal(1, response.Id);
        }

        [Fact]
        public async Task Post_Should_Return_400_On_Invalid_Model()
        {
            var message = new ContactMessage
            {
                Id = 1,
                Name = "Test",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true,
                CreatedAt = DateTime.UtcNow,
                EmailSent = false
            };
            var service = new FixedResultContactService(_ => Task.FromResult(message));
            var logger = new TestLogger<ContactController>();
            var controller = new ContactController(service, logger);
            controller.ModelState.AddModelError("Name", "El nombre es requerido");
            var dto = new ContactRequestDto();
            var result = await controller.Post(dto);
            // El middleware ahora retorna BadRequestResult (sin body)
            var badRequest = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task Post_Should_Return_500_On_Service_Failure()
        {
            var service = new FixedResultContactService(_ => throw new InvalidOperationException("Error interno"));
            var logger = new TestLogger<ContactController>();
            var controller = new ContactController(service, logger);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            // La excepción ahora se lanza y el middleware en producción la convierte a 500
            // En el test unitario, simplemente verificamos que se lanza
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Post(dto));
        }
    }
}
