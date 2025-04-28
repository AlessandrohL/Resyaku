using Resyaku.Domain.Enums;
using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Booking : AuditableEntity, ISoftDeletable
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string Reference { get; set; } = null!;
        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public TimeOnly EndTime { get; set; }
        public int PartySize { get; set; }
        public BookingStatus Status { get; set; }
        public string? PrivateComment { get; set; }
        public string? PublicComment { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<BookingTable> BookingTables { get; } = [];
        public ICollection<Table> Tables { get; } = [];
        public string ContactPhone { get; set; } = null!;
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Booking() { }

        public Booking(
            string reference,
            int customerId,
            DateOnly bookingDate,
            TimeOnly bookingStartTime,
            int durationInMinutes,
            int partySize,
            BookingStatus bookingStatus,
            string? privateComment,
            string? publicComment,
            string contactPhone)
        {
            Reference = reference;
            CustomerId = customerId;
            BookingDate = bookingDate;
            StartTime = bookingStartTime;
            DurationMinutes = durationInMinutes;
            EndTime = bookingStartTime.AddMinutes(durationInMinutes);
            PartySize = partySize;
            Status = bookingStatus;
            PrivateComment = privateComment;
            PublicComment = publicComment;
            ContactPhone = contactPhone;
        }
    }
}
