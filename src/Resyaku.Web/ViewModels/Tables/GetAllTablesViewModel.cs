using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Domain.Primitives;

namespace Resyaku.Web.ViewModels.Tables
{
    public class GetAllTablesViewModel(
        GetAllTablesQueryParams queryParams,
        PagedList<TableSummaryDto> pagedTables,
        List<ServiceAreaSummaryDto> serviceAreas)
    {
        public GetAllTablesQueryParams QueryParams { get; init; } = queryParams;
        public PagedList<TableSummaryDto> PagedTables { get; init; } = pagedTables;
        public List<ServiceAreaSummaryDto> ServiceAreas { get; init; } = serviceAreas;
    }
}
