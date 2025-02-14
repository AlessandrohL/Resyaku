using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Commands.CreateTable;

public record CreateTableCommand(
    string Name,
    int MinCapacity,
    int MaxCapacity,
    int ServiceAreaId,
    bool IsActive) : IRequest<Result>;
