using Resyaku.Domain.Enums;
using Resyaku.Domain.Primitives;
using MediatR;

namespace Resyaku.Application.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(
        DateTime BookingDate,
        string BookingTime,
        int Duration,
        int GuestCount,
        BookingStatus BookingStatus,
        string? PrivateComment,
        string? PublicComment,
        string CustomerName,
        string CustomerLastname,
        string CustomerPhone,
        string CustomerEmail,
        string CustomerDni,
        IEnumerable<int> TableIds
        ) : IRequest<Result<string>>;
}
