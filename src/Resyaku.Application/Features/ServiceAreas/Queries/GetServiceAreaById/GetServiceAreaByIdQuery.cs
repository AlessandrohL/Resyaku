using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.Errors;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById
{
    public record GetServiceAreaByIdQuery(int ServiceAreaId) : IRequest<Result<ServiceAreaSummaryDto>>;

    public sealed class GetServiceAreaByIdQueryHandler(IServiceAreaRepository serviceAreaRepository)
        : IRequestHandler<GetServiceAreaByIdQuery, Result<ServiceAreaSummaryDto>>
    {
        public async Task<Result<ServiceAreaSummaryDto>> Handle(
            GetServiceAreaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var existingServiceArea = await serviceAreaRepository.GetByIdAsync(request.ServiceAreaId);

            if (existingServiceArea is null)
            {
                return Result.Failure<ServiceAreaSummaryDto>(ServiceAreaErrors.NotFound);
            }

            return Result.Success(existingServiceArea.ToServiceAreaSummary());
        }
    }
}
