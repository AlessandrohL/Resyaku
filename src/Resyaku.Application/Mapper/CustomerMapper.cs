using Resyaku.Application.DTOs.Customers;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper;

public static class CustomerMapper
{
    public static CustomerInfoDto ToCustomerInfo(this Customer customer)
    {
        return new CustomerInfoDto(
            customer.Name, 
            customer.Lastname, 
            customer.Dni, 
            customer.Email, 
            customer.Phone);
    }
}
