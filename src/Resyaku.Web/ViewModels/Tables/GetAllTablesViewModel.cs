using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Domain.Primitives;

namespace Resyaku.Web.ViewModels.Tables
{
    public class GetAllTablesViewModel(
        GetAllTablesQueryParams queryParameters,
        PagedList<TableSummaryDto> pagedTables,
        List<ServiceAreaSummaryDto> serviceAreas)
    {
        public GetAllTablesQueryParams QueryParameters { get; init; } = queryParameters;
        public PagedList<TableSummaryDto> PagedTables { get; init; } = pagedTables;
        public List<ServiceAreaSummaryDto> ServiceAreas { get; init; } = serviceAreas;
    }
}
