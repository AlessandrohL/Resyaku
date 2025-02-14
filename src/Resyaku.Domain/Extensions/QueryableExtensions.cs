using System.Linq.Expressions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> queryable,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                return queryable.Where(predicate);
            }

            return queryable;
        }

        public static IQueryable<T> ApplyOrdering<T>(
            this IQueryable<T> query,
            IQueryFilter<T> filter)
        {
            var sortExpression = filter.GetSortProperty();
            var sortOrder = filter.SortOrder;

            if (sortOrder?.ToLower() == "desc")
            {
                return query.OrderByDescending(sortExpression);
            }

            return query.OrderBy(sortExpression);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, IQueryFilter<T> filter)
        {
            query = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return query;
        }
    }
}
