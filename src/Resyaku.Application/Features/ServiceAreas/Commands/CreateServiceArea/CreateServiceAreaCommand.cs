using Resyaku.Domain.Primitives;
using MediatR;

namespace Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea
{
    public record CreateServiceAreaCommand(string Name) : IRequest<Result>;
}
