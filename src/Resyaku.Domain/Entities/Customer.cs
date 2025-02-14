using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Customer : IAuditableEntity, ISoftDeletable
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public ICollection<Booking> Bookings { get; } = [];
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string RowUlid { get; init; } = null!;

        private Customer() { }

        public static Customer Create(
            string name,
            string lastname,
            string phone,
            string email,
            string dni,
            string rowUlid)
        {
            return new Customer
            {
                Name = name,
                Lastname = lastname,
                Phone = phone,
                Email = email,
                Dni = dni,
                RowUlid = rowUlid
            };
        }
    }
}
