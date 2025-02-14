namespace Resyaku.Domain.Primitives
{
    public class PagedList<TItem>(
        IEnumerable<TItem> items,
        int page,
        int pageSize,
        int totalCount) where TItem : class
    {
        public IEnumerable<TItem> Items { get; init; } = items;
        public int Page { get; init; } = page;
        public int PageSize { get; init; } = pageSize;
        public int TotalCount { get; init; } = totalCount;
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

    }
}
