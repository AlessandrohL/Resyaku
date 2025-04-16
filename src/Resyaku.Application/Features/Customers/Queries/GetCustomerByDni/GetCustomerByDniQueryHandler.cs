using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Customers;
using Resyaku.Application.Errors;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Customers.Queries.GetCustomerByDni
{
    public sealed class GetCustomerByDniQueryHandler(ICustomerRepository customerRepostitory)
        : IRequestHandler<GetCustomerByDniQuery, Result<GetCustomerByDniDto>>
    {
        public async Task<Result<GetCustomerByDniDto>> Handle(
            GetCustomerByDniQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await customerRepostitory.GetCustomerByDniAsync(request.Dni);

            if (customer is null)
            {
                return Result.Failure<GetCustomerByDniDto>(CustomerErrors.NotFound);
            }

            return Result.Success(customer.ToCustomerByDniDto());
        }
    }
}
