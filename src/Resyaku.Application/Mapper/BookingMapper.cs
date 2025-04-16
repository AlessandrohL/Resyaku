using Resyaku.Application.DTOs.Bookings;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper
{
    public static class BookingMapper
    {
        public static GetAllBookingsDto ToGetAllBookingsDto(this Booking booking)
        {
            return new GetAllBookingsDto
            {
                BookingId = booking.BookingId,
                BookingReference = booking.Reference,
                BookingDate = booking.BookingDate.Add(booking.BookingTime),
                BookingStatus = booking.Status,
                Tables = [.. booking.Tables.Select(t => t.Name)],
                CustomerName = booking.Customer.Name,
                CustomerDni = booking.Customer.Dni,
                CreationDate = booking.CreatedOnUtc
            };
        }
    }
}
