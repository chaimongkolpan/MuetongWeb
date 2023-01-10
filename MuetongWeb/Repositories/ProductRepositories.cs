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
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<Product?> GetAsync(long id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
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
