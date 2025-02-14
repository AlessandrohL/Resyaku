using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetTableById
{
    public sealed class GetTableByIdQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetTableByIdQuery, Result<GetTableByIdDto>>
    {
        public async Task<Result<GetTableByIdDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var table = await dbContext
                .Tables
                .AsNoTracking()
                .Include(t => t.ServiceArea)
                .Where(t => t.TableId == request.TableId)
                .Select(t => new GetTableByIdDto
                {
                    TableId = t.TableId,
                    Name = t.Name,
                    MinCapacity = t.MinCapacity,
                    MaxCapacity = t.MaxCapacity,
                    ServiceAreaId = t.ServiceAreaId,
                    ServiceAreaName = t.ServiceArea.Name,
                    IsActive = t.IsActive
                })
                .FirstOrDefaultAsync(CancellationToken.None);

            if (table is null)
            {
                return Result.Failure<GetTableByIdDto>(TableErrors.NotFound);
            }

            return Result.Success(table);
        }
    }
}
