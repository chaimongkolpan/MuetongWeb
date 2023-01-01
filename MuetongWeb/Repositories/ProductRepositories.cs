using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly MuetongContext _dbContext;
        public ProductRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Get
        public async Task<bool> AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAsync(List<Product> products)
        {
            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
