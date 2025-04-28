using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Table : AuditableEntity, ISoftDeletable
    {
        public int TableId { get; set; }
        public string Name { get; set; } = null!;
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public int ServiceAreaId { get; set; }
        public ServiceArea ServiceArea { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<BookingTable> BookingTables { get; } = [];
        public ICollection<Booking> Bookings { get; } = [];
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Table() { }

        public Table(
            string name,
            int minCapacity,
            int maxCapacity,
            int serviceAreaId,
            bool isActive)
        {
            Name = name;
            MinCapacity = minCapacity;
            MaxCapacity = maxCapacity;
            ServiceAreaId = serviceAreaId;
            IsActive = isActive;
        }
    }
}
