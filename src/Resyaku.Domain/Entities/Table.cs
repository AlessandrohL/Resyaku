using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Table : IAuditableEntity, ISoftDeletable
    {
        public int TableId { get; set; }
        public int ServiceAreaId { get; set; }
        public string Name { get; set; } = null!;
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public ServiceArea ServiceArea { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<BookingTable> BookingTables { get; } = [];
        public ICollection<Booking> Bookings { get; } = [];
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string RowUlid { get; init; } = null!;

        private Table() { }

        public static Table Create(
            string name,
            int minCapacity,
            int maxCapacity,
            ServiceArea serviceArea,
            bool isActive,
            string rowUlid)
        {
            var booking = new Table
            {
                Name = name,
                MinCapacity = minCapacity,
                MaxCapacity = maxCapacity,
                ServiceAreaId = serviceArea.ServiceAreaId,
                ServiceArea = serviceArea,
                IsActive = isActive,
                RowUlid = rowUlid,
                CreatedOnUtc = DateTime.UtcNow
            };

            return booking;
        }
    }
}
