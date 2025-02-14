using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class ServiceArea : IAuditableEntity, ISoftDeletable
    {
        public int ServiceAreaId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Table> Tables { get; set; } = [];
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string RowUlid { get; init; } = null!;

        private ServiceArea() { }

        public static ServiceArea Create(string name, string rowUlid)
        {
            return new ServiceArea
            {
                Name = name,
                RowUlid = rowUlid,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}
