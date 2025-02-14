using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class BookingErrors
    {
        public static Error TablesUnavailable => new(
            "Booking.TablesUnavailable",
            ErrorType.Conflict,
            "Una o más de las mesas solicitadas no están disponibles en el rango de tiempo seleccionado.");

    }
}
