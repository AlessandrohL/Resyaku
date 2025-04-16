using MediatR;
using Resyaku.Application.Data.Repositories;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas
{
    public sealed class GetAllServiceAreasQueryHandler(IServiceAreaRepository serviceAreaRepository)
        : IRequestHandler<GetAllServiceAreasQuery, List<GetAllServiceAreasDto>>
    {
        public async Task<List<GetAllServiceAreasDto>> Handle(
            GetAllServiceAreasQuery request,
            CancellationToken cancellationToken)
        {
            return await serviceAreaRepository.GetAllServiceAreasAsync();
        }
    }
}
