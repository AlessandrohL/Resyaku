using Resyaku.Application.DTOs.Bookings;
using Resyaku.Domain.Enums;

namespace Resyaku.Web.ViewModels.Bookings
{
    public class CreateBookingViewModel
    {
        public DateOnly BookingDate { get; set; }
        public string BookingTime { get; set; } = null!;
        public int Duration { get; set; }
        public int PartySize { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string? PrivateComment { get; set; }
        public string? PublicComment { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerLastname { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string CustomerDni { get; set; } = null!;
        public string TableIds { get; set; } = null!;
        public BookingAvailabilityDto? BookingAvailability { get; private set; }

        public CreateBookingViewModel() { }

        public CreateBookingViewModel(BookingAvailabilityDto bookingAvailability)
        {
            BookingAvailability = bookingAvailability;
        }

        public void SetBookingAvailability(BookingAvailabilityDto bookingAvailability)
        {
            BookingAvailability = bookingAvailability;
        }
    }
}
