using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class BookingSettings : AuditableEntity
    {
        public int BookingPrefId { get; set; }
        public int BookingTimeIncrement { get; set; }
        public TimeOnly DailyOpeningTime { get; set; }
        public TimeOnly DailyClosingTime { get; set; }
        public int MinAdvanceNoticeDays { get; set; }
        public int MaxAdvanceNoticeDays { get; set; }
        public int MaxPartySize { get; set; }
        public string ContactEmail { get; set; } = null!;
        public bool IsSpecial { get; init; } = false;

        public BookingSettings() { }

        public BookingSettings(
            int bookingTimeIncrement,
            TimeOnly dailyOpeningTime,
            TimeOnly dailyClosingTime,
            int minAdvanceNoticeDays,
            int maxAdvanceNoticeDays,
            int maxPartySize,
            string contactEmail)
        {
            BookingTimeIncrement = bookingTimeIncrement;
            DailyOpeningTime = dailyOpeningTime;
            DailyClosingTime = dailyClosingTime;
            MinAdvanceNoticeDays = minAdvanceNoticeDays;
            MaxAdvanceNoticeDays = maxAdvanceNoticeDays;
            MaxPartySize = maxPartySize;
            ContactEmail = contactEmail;
        }
    }
}
