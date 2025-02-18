using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class BookingErrors
    {
        public static Error TablesUnavailable => new(
            "Booking.TablesUnavailable",
            ErrorType.Conflict,
            "Una o más de las mesas seleccionadas no están disponibles en el rango de tiempo seleccionado.");

        public static Error CreationFailed => new(
            "Booking.CreationFailed",
            ErrorType.Problem,
            "Ocurrió un error inesperado al crear la reserva.");
    }
}
