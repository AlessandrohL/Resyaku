using System.Linq.Expressions;
using Resyaku.Application.DTOs.Bookings;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Queries
{
    public static class BookingQueries
    {
        public static Expression<Func<Booking, BookingEventDto>> ProjectToBookingEvent()
        {
            return b => new BookingEventDto
            {
                Reference = b.Reference,
                PartySize = b.PartySize,
                Tables = b.Tables.Select(t => t.Name).ToArray(),
                Date = b.BookingDate,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Status = b.Status.ToString(),
                CustomerName = b.Customer.Name,
                CustomerEmail = b.Customer.Email,
                CustomerDni = b.Customer.Dni
            };
        }
    }
}
