using System.Linq.Expressions;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetAllTables
{
    public sealed class GetAllTablesQueryParams : PaginationParameters, IQueryFilter<Table>
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }

        public int ServiceAreaId { get; set; }
        public bool? Enabled { get; set; }

        public Expression<Func<Table, object>> GetSortProperty()
        {
            return SortColumn?.ToLower() switch
            {
                "name" => table => table.Name,
                "createAt" => table => table.CreatedAt,
                _ => table => table.CreatedAt
            };
        }
    }
}
