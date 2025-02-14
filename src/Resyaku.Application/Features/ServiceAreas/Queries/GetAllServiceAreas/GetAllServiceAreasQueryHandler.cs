using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas
{
    public sealed class GetAllServiceAreasQueryHandler(
        IApplicationDbContext dbContext)
        : IRequestHandler<GetAllServiceAreasQuery, List<GetAllServiceAreasDto>>
    {
        public async Task<List<GetAllServiceAreasDto>> Handle(
            GetAllServiceAreasQuery request,
            CancellationToken cancellationToken)
        {
            return await dbContext
                .ServiceAreas
                .AsNoTracking()
                .Select(sa => new GetAllServiceAreasDto
                {
                    Id = sa.ServiceAreaId,
                    Name = sa.Name
                })
                .ToListAsync(CancellationToken.None);
        }
    }
}
