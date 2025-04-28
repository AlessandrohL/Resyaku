using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class Customer : AuditableEntity, ISoftDeletable
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public ICollection<Booking> Bookings { get; } = [];
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Customer() { }

        public Customer(
            string name,
            string lastname,
            string phone,
            string email,
            string dni)
        {
            Name = name;
            Lastname = lastname;
            Phone = phone;
            Email = email;
            Dni = dni;
        }
    }
}
