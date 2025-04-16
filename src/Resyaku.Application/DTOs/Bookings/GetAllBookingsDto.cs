using Resyaku.Domain.Enums;

namespace Resyaku.Application.DTOs.Bookings
{
    public class GetAllBookingsDto
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string[] Tables { get; set; } = [];
        public string CustomerDni { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
    }
}
