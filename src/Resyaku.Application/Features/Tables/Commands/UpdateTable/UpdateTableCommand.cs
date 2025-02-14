using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.UpdateTable
{
    public record UpdateTableCommand(
        int TableId,
        string Name,
        int MinCapacity,
        int MaxCapacity,
        int ServiceAreaId,
        bool IsActive) : IRequest<Result>;
}
