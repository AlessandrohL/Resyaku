namespace Resyaku.Domain.Primitives
{
    public interface IAuditableEntity
    {
        string RowUlid { get; init; }
        DateTime CreatedOnUtc { get; init; }
        DateTime? ModifiedOnUtc { get; set; }
    }
}
