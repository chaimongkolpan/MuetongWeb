using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IProductRepositories
    {
        Task<bool> AddAsync(Product product);
        Task<bool> AddAsync(List<Product> products);
    }
}
