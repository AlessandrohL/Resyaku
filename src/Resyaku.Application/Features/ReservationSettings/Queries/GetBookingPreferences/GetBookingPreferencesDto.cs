namespace Resyaku.Application.Features.ReservationSettings.Queries.GetBookingPreferences
{
    public class GetBookingPreferencesDto
    {
        public int BookingTimeIncrement { get; set; }
        public int MaxGuests { get; set; }
        public int MinAdvanceNotice { get; set; }
        public int MaxDaysInAdvance { get; set; }
        public string ContactEmail { get; set; } = null!;
    }
}