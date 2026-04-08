using backend.Models;
using backend.Validators;
using FluentValidation;
using Xunit;

namespace backend.Tests
{
    // Feature: tattoo-studio-backend, Property 1: Valid input acceptance
    public class ContactRequestAcceptancePropertyTests
    {
        [Fact]
        public void ContactRequestDto_With_Valid_Fields_Should_Be_Valid()
        {
            var validator = new ContactRequestValidator();
            var dto = new ContactRequestDto
            {
                Name = "Juan Perez",
                Email = "juan.perez@example.com",
                Phone = "+56912345678",
                Message = "Quiero consultar por una cita.",
                WantsAppointment = true
            };
            var result = validator.Validate(dto);
            Assert.True(result.IsValid);
        }
    }
}
