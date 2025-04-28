namespace Resyaku.Domain.Primitives
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public Guid RowGuid { get; init; } = Guid.NewGuid();
    }
}
