using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetAllTables
{
    public sealed class GetAllTablesQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetAllTablesQuery, PagedList<GetAllTablesDto>>
    {
        public async Task<PagedList<GetAllTablesDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var parameters = request.QueryParameters;
            var query = dbContext.Tables
                .AsNoTracking()
                .Include(t => t.ServiceArea)
                .WhereIf(!string.IsNullOrWhiteSpace(parameters.SearchTerm), t => EF.Functions.Like(t.Name, $"%{parameters.SearchTerm}%"))
                .WhereIf(parameters.ServiceAreaId != 0, t => t.ServiceAreaId == parameters.ServiceAreaId);

            int count = await query.CountAsync(CancellationToken.None);

            List<GetAllTablesDto> tables = await query
                .Include(t => t.ServiceArea)
                .ApplyOrdering(parameters)
                .ApplyPagination(parameters)
                .Select(t => new GetAllTablesDto
                {
                    TableId = t.TableId,
                    Name = t.Name,
                    MinCapacity = t.MinCapacity,
                    MaxCapacity = t.MaxCapacity,
                    ServiceAreaId = t.ServiceAreaId,
                    ServiceAreaName = t.ServiceArea.Name,
                    IsActive = t.IsActive
                })
                .ToListAsync(CancellationToken.None);

            return new PagedList<GetAllTablesDto>(tables, parameters.Page, parameters.PageSize, count);
        }
    }
}
