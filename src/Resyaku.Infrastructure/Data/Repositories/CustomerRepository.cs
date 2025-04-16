using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Domain.Entities;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
    {
        public async Task<Customer?> GetCustomerByDniAsync(string dni)
        {
            return await dbContext.Customers
                .AsNoTracking()
                .Where(c => c.Dni == dni)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await dbContext.Customers.AnyAsync(c => c.Email == email);
        }

        public async Task<int> GetCustomerIdByDniAsync(string dni)
        {
            return await dbContext.Customers
                .Where(c => c.Dni == dni)
                .Select(c => c.CustomerId)
                .FirstOrDefaultAsync();
        }

        public void Add(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }

        public void Remove(Customer customer)
        {
            dbContext.Customers.Remove(customer);
        }
    }
}
