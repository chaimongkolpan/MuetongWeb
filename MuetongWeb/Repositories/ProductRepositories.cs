using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly MuetongContext _dbContext;
        public ProductRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAsync(ProductRequest request)
        {
            return await _dbContext.Products.Where(prod => (string.IsNullOrWhiteSpace(request.Query)
                                    || (prod.Name ?? "").Contains(request.Query))
                                    && prod.Id != 0
                                )
                                //.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
                                .Include(prod => prod.PrDetails)
                                .Include(prod => prod.PoDetails)
                                .ToListAsync();
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
        public async Task<bool> UpdateAsync(Product product)
        {
            var tmp = await _dbContext.Products.FindAsync(product.Id);
            if (tmp == null)
                return false;
            tmp.Name = product.Name;
            tmp.Unit = product.Unit;
            tmp.ModifyDate = product.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return false;
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
