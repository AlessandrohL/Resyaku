namespace Resyaku.Domain.Primitives
{
    public class CollectionResult<T>(IReadOnlyList<T> items, int totalCount)
    {
        public IReadOnlyList<T> Items { get; } = items;
        public int TotalCount { get; } = totalCount;
    }
}
