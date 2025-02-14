using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Tables.Queries.GetAllTables
{
    public record GetAllTablesQuery(GetAllTablesQueryParameters QueryParameters)
        : IRequest<PagedList<GetAllTablesDto>>;
}
