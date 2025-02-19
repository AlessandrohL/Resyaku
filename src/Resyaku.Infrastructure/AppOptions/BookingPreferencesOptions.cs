namespace Resyaku.Infrastructure.AppOptions
{
    public class BookingPreferencesOptions
    {
        public int BookingTimeIncrement { get; set; }
        public TimeSpan DailyOpeningTime { get; set; }
        public TimeSpan DailyClosingTime { get; set; }
        public int MaxGuests { get; set; }
        public int MinAdvanceNotice { get; set; }
        public int MaxDaysInAdvance { get; set; }
        public string ContactEmail { get; set; } = null!;
    }
}
