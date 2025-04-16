using Resyaku.Domain.Entities;

namespace Resyaku.Application.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByDniAsync(string dni);
        Task<bool> ExistsByEmailAsync(string email);
        Task<int> GetCustomerIdByDniAsync(string dni);
        void Add(Customer customer);
        void Remove(Customer customer);
    }
}
