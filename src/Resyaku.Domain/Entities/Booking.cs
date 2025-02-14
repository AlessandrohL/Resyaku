using Resyaku.Domain.Enums;
using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Booking : IAuditableEntity, ISoftDeletable
    {
        private Booking() { }

        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string Reference { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndTime { get; private set; }
        public int GuestCount { get; set; }
        public BookingStatus Status { get; set; }
        public string? PrivateComment { get; set; }
        public string? PublicComment { get; set; }
        public Customer Customer { get; private set; } = null!;
        public ICollection<BookingTable> BookingTables { get; } = [];
        public ICollection<Table> Tables { get; } = [];
        public string ContactPhone { get; set; } = null!;
        public bool IsConfirmed { get; set; }
        public bool IsWalking { get; set; }
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string RowUlid { get; init; } = null!;

        public static Booking Create(
            string bookingReference,
            DateTime bookingDate,
            TimeSpan bookingStartTime,
            int durationInMinutes,
            int guestCount,
            BookingStatus bookingStatus,
            string? privateComment,
            string? publicComment,
            Customer customer,
            string contactPhone,
            bool isWalking,
            string rowUlid)
        {
            Booking booking = new()
            {
                Reference = bookingReference,
                BookingDate = bookingDate,
                BookingTime = bookingStartTime,
                Duration = durationInMinutes,
                EndTime = bookingDate
                    .Add(bookingStartTime)
                    .AddMinutes(durationInMinutes),
                GuestCount = guestCount,
                Status = bookingStatus,
                PrivateComment = privateComment,
                PublicComment = publicComment,
                Customer = customer,
                ContactPhone = contactPhone,
                IsWalking = isWalking,
                CreatedOnUtc = DateTime.UtcNow,
                RowUlid = rowUlid
            };

            return booking;
        }
    }
}
