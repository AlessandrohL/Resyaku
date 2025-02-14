using FluentValidation;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public sealed class GetAvailableTablesQueryValidator
        : AbstractValidator<GetAvailableTablesQueryParams>
    {
        public GetAvailableTablesQueryValidator()
        {
            RuleFor(p => p.BookingDate)
                .NotEmpty()
                .WithMessage("La fecha de reserva es obligatoria.")
                .Must(bookingDatetime => bookingDatetime.Date >= DateTime.Now.Date)
                .WithMessage("La fecha de reserva no puede ser anterior a hoy.");

            RuleFor(p => p.BookingTime)
                .NotEmpty()
                .WithMessage("La hora de reserva es obligatoria.")
                .Must(bookingTime => TimeSpan.TryParse(bookingTime, out _))
                .WithMessage("La hora de reserva debe tener un formato válido");

            RuleFor(p => p.Duration)
                .NotEmpty()
                .WithMessage("La duración es obligatoria.")
                .GreaterThan(0)
                .WithMessage("La duración debe ser mayor que 0.");
        }
    }
}
