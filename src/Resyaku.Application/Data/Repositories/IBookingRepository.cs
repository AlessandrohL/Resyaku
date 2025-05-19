using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Queries.GetAllBookingEvents;
using Resyaku.Application.Features.Bookings.Queries.GetAllBookings;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Data.Repositories
{
    public interface IBookingRepository
    {
        Task<CollectionResult<BookingSummaryDto>> GetAllBookingsAsync(GetAllBookingsQueryParams queryParams);
        Task<IReadOnlyList<BookingEventDto>> GetAllBookingEventsAsync(GetAllBookingEventsQueryParams queryParams);
        void Add(Booking booking);
        void Remove(Booking booking);
    }
}
