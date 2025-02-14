using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById
{
    public sealed class GetServiceAreaByIdQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetServiceAreaByIdQuery, Result<GetServiceAreaByIdDto>>
    {
        public async Task<Result<GetServiceAreaByIdDto>> Handle(
            GetServiceAreaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var serviceArea = await dbContext
                .ServiceAreas
                .AsNoTracking()
                .Where(sa => sa.ServiceAreaId == request.ServiceAreaId)
                .Select(sa => new GetServiceAreaByIdDto
                {
                    Id = sa.ServiceAreaId,
                    Name = sa.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (serviceArea is null)
            {
                return Result.Failure<GetServiceAreaByIdDto>(ServiceAreaErrors.NotFound);
            }

            return Result.Success(serviceArea);
        }
    }
}
