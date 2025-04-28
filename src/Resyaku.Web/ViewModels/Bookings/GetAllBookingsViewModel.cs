using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Queries.GetAllBookings;
using Resyaku.Domain.Primitives;

namespace Resyaku.Web.ViewModels.Bookings
{
    public class GetAllBookingsViewModel(
        GetAllBookingsQueryParams queryParameters,
        PagedList<BookingSummaryDto> bookingResponses)
    {
        public GetAllBookingsQueryParams QueryParameters { get; init; } = queryParameters;
        public PagedList<BookingSummaryDto> BookingResponses { get; init; } = bookingResponses;
    }
}
