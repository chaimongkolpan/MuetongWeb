using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface ICustomerRepositories
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<IEnumerable<Customer>> GetAsync(string? query, long? provinceId, int page, int pageSize);
        Task<int> CountAsync(string? query, long? provinceId);
        Task<Customer?> GetAsync(long id);
        Task<bool> AddAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(long id);
    }
}
