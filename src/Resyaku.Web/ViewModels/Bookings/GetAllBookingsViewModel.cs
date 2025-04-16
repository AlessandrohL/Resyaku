using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Queries.GetBookings;
using Resyaku.Domain.Primitives;

namespace Resyaku.Web.ViewModels.Bookings
{
    public class GetAllBookingsViewModel(
        GetAllBookingsQueryParams queryParameters,
        PagedList<GetAllBookingsDto> bookingResponses)
    {
        public GetAllBookingsQueryParams QueryParameters { get; init; } = queryParameters;
        public PagedList<GetAllBookingsDto> BookingResponses { get; init; } = bookingResponses;
    }
}
