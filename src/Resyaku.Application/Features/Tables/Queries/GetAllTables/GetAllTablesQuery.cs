using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetAllTables
{
    public record GetAllTablesQuery(GetAllTablesQueryParams QueryParameters)
        : IRequest<PagedList<TableSummaryDto>>;

    public sealed class GetAllTablesQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetAllTablesQuery, PagedList<TableSummaryDto>>
    {
        public async Task<PagedList<TableSummaryDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tablesCollectionResult = await tableRepository.GetAllTablesAsync(request.QueryParameters);

            return new PagedList<TableSummaryDto>(
                items: tablesCollectionResult.Items,
                page: request.QueryParameters.Page,
                pageSize: request.QueryParameters.PageSize,
                totalCount: tablesCollectionResult.TotalCount);
        }
    }
}
