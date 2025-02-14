using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetTableById
{
    public record GetTableByIdQuery(int TableId) : IRequest<Result<GetTableByIdDto>>;
}
