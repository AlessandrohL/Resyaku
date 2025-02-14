using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Domain.Primitives;

namespace Resyaku.Web.ViewModels.Tables
{
    public class GetAllTablesViewModel(
        GetAllTablesQueryParameters queryParameters,
        PagedList<GetAllTablesDto> pagedTables)
    {
        public GetAllTablesQueryParameters QueryParameters { get; init; } = queryParameters;
        public PagedList<GetAllTablesDto> PagedTables { get; init; } = pagedTables;
    }
}
