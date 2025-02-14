using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class BookingPreferences : IAuditableEntity
    {
        public int BookingPrefId { get; set; }
        public int BookingTimeIncrement { get; set; }
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
            int maxGuests,
            int minAdvanceNotice,
            int maxDaysInAdvance,
            string contactEmail,
            string rowUlid)
        {
            return new BookingPreferences
            {
                BookingTimeIncrement = bookingTimeIncrement,
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
