using FluentValidation;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public sealed class GetAvailableTablesValidator
        : AbstractValidator<GetAvailableTablesQueryParams>
    {
        public GetAvailableTablesValidator()
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
        }
    }
}
