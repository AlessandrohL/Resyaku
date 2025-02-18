using MediatR;

namespace Resyaku.Application.Features.Bookings.Queries.GetBookingAvailability
{
    public record GetBookingAvailabilityQuery : IRequest<BookingAvailabilityDto>;
}
