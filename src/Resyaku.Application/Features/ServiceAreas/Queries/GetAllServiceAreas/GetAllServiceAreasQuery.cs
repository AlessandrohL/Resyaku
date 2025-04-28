using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.ServiceAreas;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas
{
    public record GetAllServiceAreasQuery() : IRequest<List<ServiceAreaSummaryDto>>;

    public sealed class GetAllServiceAreasQueryHandler(IServiceAreaRepository serviceAreaRepository)
        : IRequestHandler<GetAllServiceAreasQuery, List<ServiceAreaSummaryDto>>
    {
        public async Task<List<ServiceAreaSummaryDto>> Handle(
            GetAllServiceAreasQuery request,
            CancellationToken cancellationToken)
        {
            return await serviceAreaRepository.GetAllServiceAreasAsync();
        }
    }
}
