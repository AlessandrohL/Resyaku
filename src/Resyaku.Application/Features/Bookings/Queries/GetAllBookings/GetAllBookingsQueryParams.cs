using System.Linq.Expressions;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookings
{
    public sealed class GetAllBookingsQueryParams : PaginationParameters, IQueryFilter<Booking>
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerDni { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Status { get; set; }

        public Expression<Func<Booking, object>> GetSortProperty()
        {
            return SortColumn?.ToLower() switch
            {
                "bookingDate" => booking => booking.BookingDate,
                "createdAt" => booking => booking.CreatedAt,
                _ => booking => booking.BookingDate
            };
        }
    }
}
