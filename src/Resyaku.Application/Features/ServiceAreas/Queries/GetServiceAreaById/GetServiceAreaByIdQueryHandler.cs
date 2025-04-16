using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Errors;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById
{
    public sealed class GetServiceAreaByIdQueryHandler(IServiceAreaRepository serviceAreaRepository)
        : IRequestHandler<GetServiceAreaByIdQuery, Result<GetServiceAreaByIdDto>>
    {
        public async Task<Result<GetServiceAreaByIdDto>> Handle(
            GetServiceAreaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var existingServiceArea = await serviceAreaRepository.GetByIdAsync(request.ServiceAreaId);

            if (existingServiceArea is null)
            {
                return Result.Failure<GetServiceAreaByIdDto>(ServiceAreaErrors.NotFound);
            }

            return Result.Success(existingServiceArea.ToServiceAreaByIdDto());
        }
    }
}
