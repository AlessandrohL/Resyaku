using MediatR;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas
{
    public record GetAllServiceAreasQuery() : IRequest<List<GetAllServiceAreasDto>>;
}
