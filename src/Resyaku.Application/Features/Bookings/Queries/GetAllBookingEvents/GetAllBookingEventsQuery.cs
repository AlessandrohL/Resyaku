using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Bookings;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookingEvents
{
    public sealed record GetAllBookingEventsQuery(GetAllBookingEventsQueryParams QueryParams)
        : IRequest<List<BookingEventDto>>;

    public sealed class GetAllBookingEventsQueryHandler(IBookingRepository bookingRepository)
        : IRequestHandler<GetAllBookingEventsQuery, List<BookingEventDto>>
    {
        public async Task<List<BookingEventDto>> Handle(
            GetAllBookingEventsQuery request, 
            CancellationToken cancellationToken)
        {
            var bookings = await bookingRepository.GetAllBookingEventsAsync(request.QueryParams);
            return bookings.ToList();
        }
    }
}
