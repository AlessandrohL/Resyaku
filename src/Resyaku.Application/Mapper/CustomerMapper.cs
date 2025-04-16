using Resyaku.Application.DTOs.Customers;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper
{
    public static class CustomerMapper
    {
        public static GetCustomerByDniDto ToCustomerByDniDto(this Customer customer)
        {
            return new GetCustomerByDniDto
            {
                CustomerName = customer.Name,
                CustomerLastname = customer.Lastname,
                CustomerDni = customer.Dni,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.Phone
            };
        }
    }
}
