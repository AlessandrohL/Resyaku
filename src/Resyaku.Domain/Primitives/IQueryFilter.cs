using System.Linq.Expressions;

namespace Resyaku.Domain.Primitives
{
    public interface IQueryFilter<T>
    {
        string? SearchTerm { get; set; }
        string? SortColumn { get; set; }
        string? SortOrder { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }

        Expression<Func<T, object>> GetSortProperty();
    }
}
