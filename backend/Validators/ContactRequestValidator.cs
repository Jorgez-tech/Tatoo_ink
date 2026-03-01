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
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres")
                .Matches(@"^(?!.*\.\.)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")
                .WithMessage("El formato del correo electrónico no es válido (RFC 5322)");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El teléfono es requerido")
                .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("El mensaje es requerido")
                .MaximumLength(1000).WithMessage("El mensaje no puede exceder 1000 caracteres");

            RuleFor(x => x.WantsAppointment)
                .Custom((value, context) => {
                    if (value == null)
                    {
                        context.AddFailure("Debe indicar si desea agendar una cita");
                    }
                });
        }
    }
}
