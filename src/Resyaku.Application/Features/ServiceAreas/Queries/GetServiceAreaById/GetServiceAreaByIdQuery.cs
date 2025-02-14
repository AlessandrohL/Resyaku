using MediatR;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById
{
    public record GetServiceAreaByIdQuery(int ServiceAreaId) : IRequest<Result<GetServiceAreaByIdDto>>;
}
