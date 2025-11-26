using backend.Models;
using backend.Validators;
using FluentValidation;
using Xunit;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 2: Email format validation
    public class ContactRequestEmailFormatPropertyTests
    {
        [Theory]
        [InlineData("usuario@dominio.com", true)]
        [InlineData("usuario@dominio", false)]
        [InlineData("usuario.com", false)]
        [InlineData("usuario@dominio.co.uk", true)]
        [InlineData("usuario@dominio..com", false)]
        [InlineData("usuario@dominio.c", false)]
        [InlineData("usuario@dominio.com.", false)]
        [InlineData("usuario@dominio.com", true)]
        [InlineData("usuario@sub.dominio.com", true)]
        [InlineData("usuario@dominio", false)]
        public void ContactRequestDto_Email_Format_Should_Be_Validated(string email, bool expectedValid)
        {
            var validator = new ContactRequestValidator();
            var dto = new ContactRequestDto
            {
                Name = "Test",
                Email = email,
                Phone = "123",
                Message = "Mensaje de prueba",
                WantsAppointment = true
            };
            var result = validator.Validate(dto);
            Assert.Equal(expectedValid, result.IsValid);
        }
    }
}
