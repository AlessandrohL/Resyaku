using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Queries.GetBookings;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Data.Repositories
{
    public interface IBookingRepository
    {
        Task<CollectionResult<GetAllBookingsDto>> GetAllBookingsAsync(GetAllBookingsQueryParams queryParams);
        void Add(Booking booking);
        void Remove(Booking booking);
    }
}
