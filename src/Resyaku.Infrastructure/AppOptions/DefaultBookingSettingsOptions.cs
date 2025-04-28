namespace Resyaku.Infrastructure.AppOptions
{
    public class DefaultBookingSettingsOptions
    {
        public int BookingTimeIncrement { get; init; }
        public TimeOnly DailyOpeningTime { get; init; }
        public TimeOnly DailyClosingTime { get; init; }
        public int MaxGuests { get; init; }
        public int MinAdvanceNoticeDays { get; init; }
        public int MaxAdvanceNoticeDays { get; init; }
        public string ContactEmail { get; init; } = null!;
    }
}
