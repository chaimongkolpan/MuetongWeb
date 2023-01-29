using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Product>> GetAsync(ProductRequest request);
        Task<IEnumerable<Product>> GetAsync();
        Task<Product?> GetAsync(long id);
        Task<bool> AddAsync(Product product);
        Task<bool> AddAsync(List<Product> products);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(long id);
    }
}
