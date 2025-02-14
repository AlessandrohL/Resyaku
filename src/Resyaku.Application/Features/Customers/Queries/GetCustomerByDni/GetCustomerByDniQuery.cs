using Resyaku.Domain.Primitives;
using MediatR;

namespace Resyaku.Application.Features.Customers.Queries.GetCustomerByDni
{
    public record GetCustomerByDniQuery(string Dni) : IRequest<Result<GetCustomerByDniDto>>;
}
