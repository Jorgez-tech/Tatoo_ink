using backend.Models;
using backend.Validators;
using FluentValidation;
using Xunit;
using System.Reflection;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 3: Invalid input rejection
    public class ContactRequestInvalidInputPropertyTests
    {
        [Theory]
        [InlineData("", "test@test.com", "123", "Mensaje", true)] // Nombre vacío
        [InlineData("Juan", "", "123", "Mensaje", true)] // Email vacío
        [InlineData("Juan", "test@test.com", "", "Mensaje", true)] // Teléfono vacío
        [InlineData("Juan", "test@test.com", "123", "", true)] // Mensaje vacío
        [InlineData("Juan", "test@test.com", "123", "Mensaje", null)] // WantsAppointment null
        [InlineData("a", "test@test.com", "123", "Mensaje", true)] // Nombre < 2
        [InlineData("Juan", "usuario@dominio..com", "123", "Mensaje", true)] // Email inválido
        public void ContactRequestDto_With_Invalid_Fields_Should_Be_Rejected(
            string name, string email, string phone, string message, bool? wantsAppointment)
        {
            var validator = new ContactRequestValidator();
            var dto = new ContactRequestDto
            {
                Name = name,
                Email = email,
                Phone = phone,
                Message = message
            };
            if (wantsAppointment.HasValue)
                dto.WantsAppointment = wantsAppointment.Value;
            else
            {
                // Forzar nulo usando reflexión
                typeof(ContactRequestDto).GetProperty("WantsAppointment")?.SetValue(dto, null);
            }
            var result = validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
