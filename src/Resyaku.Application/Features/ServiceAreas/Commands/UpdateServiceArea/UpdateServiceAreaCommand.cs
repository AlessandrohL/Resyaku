using Resyaku.Domain.Primitives;
using MediatR;

namespace Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea
{
    public record UpdateServiceAreaCommand(int ServiceAreaId, string Name) : IRequest<Result>;
}
