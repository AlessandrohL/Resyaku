using Resyaku.Domain.Primitives;
using MediatR;
using Resyaku.Application.DTOs.Customers;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Errors;
using Resyaku.Application.Mapper;

namespace Resyaku.Application.Features.Customers.Queries.GetCustomerByDni
{
    public record GetCustomerByDniQuery(string Dni) : IRequest<Result<CustomerInfoDto>>;

    public sealed class GetCustomerByDniQueryHandler(ICustomerRepository customerRepostitory)
        : IRequestHandler<GetCustomerByDniQuery, Result<CustomerInfoDto>>
    {
        public async Task<Result<CustomerInfoDto>> Handle(
            GetCustomerByDniQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await customerRepostitory.GetCustomerByDniAsync(request.Dni);

            if (customer is null)
            {
                return Result.Failure<CustomerInfoDto>(CustomerErrors.NotFound);
            }

            return Result.Success(customer.ToCustomerInfo());
        }
    }
}
