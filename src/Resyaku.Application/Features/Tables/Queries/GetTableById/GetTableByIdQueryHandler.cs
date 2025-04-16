using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Errors;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetTableById
{
    public sealed class GetTableByIdQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetTableByIdQuery, Result<GetTableByIdDto>>
    {
        public async Task<Result<GetTableByIdDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTable = await tableRepository.GetByIdAsync(request.TableId);

            if (existingTable is null)
            {
                return Result.Failure<GetTableByIdDto>(TableErrors.NotFound);
            }

            return Result.Success(existingTable.ToTableByIdDto());
        }
    }
}
