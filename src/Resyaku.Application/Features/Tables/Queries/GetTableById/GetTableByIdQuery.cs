using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetTableById
{
    public record GetTableByIdQuery(int TableId) : IRequest<Result<TableSummaryDto>>;

    public sealed class GetTableByIdQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetTableByIdQuery, Result<TableSummaryDto>>
    {
        public async Task<Result<TableSummaryDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTable = await tableRepository.GetTableSummaryByIdAsync(request.TableId);

            if (existingTable is null)
            {
                return Result.Failure<TableSummaryDto>(TableErrors.NotFound);
            }

            return Result.Success(existingTable);
        }
    }
}
