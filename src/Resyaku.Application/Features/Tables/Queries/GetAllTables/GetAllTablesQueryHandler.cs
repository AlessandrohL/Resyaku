using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetAllTables
{
    public sealed class GetAllTablesQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetAllTablesQuery, PagedList<GetAllTablesDto>>
    {
        public async Task<PagedList<GetAllTablesDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tablesCollectionResult = await tableRepository.GetAllTablesAsync(request.QueryParameters);

            return new PagedList<GetAllTablesDto>(
                items: tablesCollectionResult.Items,
                page: request.QueryParameters.Page,
                pageSize: request.QueryParameters.PageSize,
                totalCount: tablesCollectionResult.TotalCount);
        }
    }
}
