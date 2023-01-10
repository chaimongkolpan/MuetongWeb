using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product?> GetAsync(long id);
        Task<bool> AddAsync(Product product);
        Task<bool> AddAsync(List<Product> products);
    }
}
