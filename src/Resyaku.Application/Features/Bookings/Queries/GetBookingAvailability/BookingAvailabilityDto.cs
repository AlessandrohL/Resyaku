namespace Resyaku.Application.Features.Bookings.Queries.GetBookingAvailability
{
    public class BookingAvailabilityDto
    {
        public IList<TimeSpan> AvailableBookingTimes { get; set; } = [];
        public IList<int> AvailableDurations { get; set; } = [];
        public IList<int> AvailableGuestCounts { get; set; } = [];
    }
}
