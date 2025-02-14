namespace Resyaku.Domain.Entities
{
    public sealed class BookingTable
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
    }
}
