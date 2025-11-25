using backend.Models;
using FluentValidation;

namespace backend.Validators
{
    public class ContactRequestValidator : AbstractValidator<ContactRequestDto>
    {
        public ContactRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El teléfono es requerido")
                .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("El mensaje es requerido")
                .MaximumLength(1000).WithMessage("El mensaje no puede exceder 1000 caracteres");

            RuleFor(x => x.WantsAppointment)
                .NotNull().WithMessage("Debe indicar si desea agendar una cita");
        }
    }
}
