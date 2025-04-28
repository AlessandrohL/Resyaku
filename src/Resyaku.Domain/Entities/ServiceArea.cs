using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class ServiceArea : AuditableEntity, ISoftDeletable
    {
        public int ServiceAreaId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Table> Tables { get; set; } = [];
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ServiceArea() { }

        public ServiceArea(string name)
        {
            Name = name;
        }
    }
}
