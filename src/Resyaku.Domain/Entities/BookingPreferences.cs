using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class BookingPreferences : IAuditableEntity
    {
        public int BookingPrefId { get; set; }
        public int BookingTimeIncrement { get; set; }
        public TimeSpan DailyOpeningTime { get; set; }
        public TimeSpan DailyClosingTime { get; set; }
        public int MaxGuests { get; set; }
        public int MinAdvanceNotice { get; set; }
        public int MaxDaysInAdvance { get; set; }
        public string ContactEmail { get; set; } = null!;
        public string RowUlid { get; init; } = null!;
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }

        public bool IsSpecial { get; init; } = false;

        private BookingPreferences() { }

        public static BookingPreferences Create(
            int bookingTimeIncrement,
            TimeSpan dailyOpeningTime,
            TimeSpan dailyClosingTime,
            int maxGuests,
            int minAdvanceNotice,
            int maxDaysInAdvance,
            string contactEmail,
            string rowUlid)
        {
            return new BookingPreferences
            {
                BookingTimeIncrement = bookingTimeIncrement,
                DailyOpeningTime = dailyOpeningTime,
                DailyClosingTime = dailyClosingTime,
                MaxGuests = maxGuests,
                MinAdvanceNotice = minAdvanceNotice,
                MaxDaysInAdvance = maxDaysInAdvance,
                ContactEmail = contactEmail,
                RowUlid = rowUlid,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}
