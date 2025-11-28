using backend.Controllers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace backend.Tests
{
    public class ContactControllerUnitTests
    {
        [Fact]
        public async Task Post_Should_Return_200_On_Valid_Input()
        {
            var serviceMock = new Mock<IContactService>();
            serviceMock.Setup(x => x.ProcessContactMessageAsync(It.IsAny<ContactRequestDto>()))
                .ReturnsAsync(new ServiceResult { Success = true, Id = 1 });
            var loggerMock = new Mock<ILogger<ContactController>>();
            var controller = new ContactController(serviceMock.Object, loggerMock.Object);
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
            var serviceMock = new Mock<IContactService>();
            var loggerMock = new Mock<ILogger<ContactController>>();
            var controller = new ContactController(serviceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Name", "El nombre es requerido");
            var dto = new ContactRequestDto();
            var result = await controller.Post(dto);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ContactResponseDto>(badRequest.Value);
            Assert.False(response.Success);
            Assert.Equal("Datos de entrada inválidos", response.Message);
        }

        [Fact]
        public async Task Post_Should_Return_500_On_Service_Failure()
        {
            var serviceMock = new Mock<IContactService>();
            serviceMock.Setup(x => x.ProcessContactMessageAsync(It.IsAny<ContactRequestDto>()))
                .ReturnsAsync(new ServiceResult { Success = false, Error = "Error interno" });
            var loggerMock = new Mock<ILogger<ContactController>>();
            var controller = new ContactController(serviceMock.Object, loggerMock.Object);
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = "test@email.com",
                Phone = "123456",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = await controller.Post(dto);
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);
            var response = Assert.IsType<ContactResponseDto>(errorResult.Value);
            Assert.False(response.Success);
            Assert.Contains("error", response.Message.ToLower());
        }
    }
}
