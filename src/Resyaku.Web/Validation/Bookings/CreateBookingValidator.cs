using FluentValidation;
using Resyaku.Web.ViewModels.Bookings;

namespace Resyaku.Web.Validation.Bookings
{
    public sealed class CreateBookingValidator : AbstractValidator<CreateBookingViewModel>
    {
        public CreateBookingValidator()
        {
            RuleFor(p => p.BookingDate)
                .NotEmpty()
                .WithMessage("La fecha de reserva es obligatoria.")
                .Must(bookingDate => bookingDate >= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("La fecha de reserva no puede ser anterior a hoy.");

            RuleFor(p => p.BookingTime)
                .NotEmpty()
                .WithMessage("La hora de reserva es obligatoria.")
                .Must(bookingTime => TimeOnly.TryParse(bookingTime, out _))
                .WithMessage("La hora de reserva debe tener un formato válido");

            RuleFor(p => p.Duration)
                .NotEmpty()
                .WithMessage("La duración es obligatoria.")
                .GreaterThan(0)
                .WithMessage("La duración debe ser mayor que 0.");

            RuleFor(p => p.PartySize)
                .NotEmpty()
                .WithMessage("El número de invitados es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El número de invitados debe ser mayor que 0.");

            RuleFor(p => p.BookingStatus)
                .IsInEnum()
                .WithMessage("El estado de la reserva debe ser un valor válido.");

            RuleFor(p => p.PrivateComment)
                .Length(3, 250)
                .WithMessage("wdawd")
                .When(p => !string.IsNullOrWhiteSpace(p.PrivateComment));

            RuleFor(p => p.PublicComment)
                .Length(3, 250)
                .WithMessage("dawdawd")
                .When(p => !string.IsNullOrWhiteSpace(p.PublicComment));

            RuleFor(p => p.CustomerDni)
                 .NotEmpty()
                 .WithMessage("El DNI es obligatorio.")
                 .Length(8)
                 .WithMessage("El DNI debe tener 8 digitos.");

            RuleFor(p => p.CustomerName)
                .NotEmpty()
                .WithMessage("El nombre del cliente es obligatorio.")
                .Length(2, 40)
                .WithMessage("El nombre debe tener entre 2 y 40 caracteres.");

            RuleFor(p => p.CustomerLastname)
                .NotEmpty()
                .WithMessage("El apellido del cliente es obligatorio.")
                .Length(2, 40)
                .WithMessage("El apellido debe tener entre 2 y 40 caracteres.");

            RuleFor(p => p.CustomerPhone)
                .NotEmpty()
                .WithMessage("El teléfono del cliente es obligatorio.")
                .Length(9)
                .WithMessage("El teléfono debe tener 9 digitos.");

            RuleFor(p => p.CustomerEmail)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                .WithMessage("El formato es inválido.")
                .MaximumLength(100)
                .WithMessage("El correo electrónico no puede tener más de 100 caracteres.");

            RuleFor(p => p.TableIds)
                .NotEmpty()
                .WithMessage("No se ha seleccionado ninguna mesa.")
                .Matches(@"^\d+(,\d+)*$")
                .WithMessage("La lista tiene un formato invalido.");
        }
    }
}
