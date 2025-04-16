using Resyaku.Domain.Primitives;
using MediatR;
using Resyaku.Application.DTOs.Customers;

namespace Resyaku.Application.Features.Customers.Queries.GetCustomerByDni
{
    public record GetCustomerByDniQuery(string Dni) : IRequest<Result<GetCustomerByDniDto>>;
}
