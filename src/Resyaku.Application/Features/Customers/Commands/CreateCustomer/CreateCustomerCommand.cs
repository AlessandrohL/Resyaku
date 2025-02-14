using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(
        string Firstname,
        string Lastname,
        string Phone,
        string Email,
        string Dni) : IRequest<Result<int>>;
}
