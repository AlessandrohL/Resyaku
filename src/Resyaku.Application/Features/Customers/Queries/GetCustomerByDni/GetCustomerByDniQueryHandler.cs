using Resyaku.Application.Data;
using Resyaku.Application.Errors;
using Resyaku.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Resyaku.Application.Features.Customers.Queries.GetCustomerByDni
{
    public sealed class GetCustomerByDniQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetCustomerByDniQuery, Result<GetCustomerByDniDto>>
    {
        public async Task<Result<GetCustomerByDniDto>> Handle(
            GetCustomerByDniQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await dbContext
                .Customers
                .AsNoTracking()
                .Where(c => c.Dni == request.Dni)
                .Select(c => new GetCustomerByDniDto
                {
                    CustomerName = c.Name,
                    CustomerLastname = c.Lastname,
                    CustomerDni = c.Dni,
                    CustomerEmail = c.Email,
                    CustomerPhone = c.Phone
                })
                .FirstOrDefaultAsync(CancellationToken.None);

            if (customer is null)
            {
                return Result.Failure<GetCustomerByDniDto>(CustomerErrors.NotFound);
            }

            return Result.Success(customer);
        }
    }
}
