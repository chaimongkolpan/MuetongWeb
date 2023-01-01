using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class StoreRepositories : IStoreRepositories
    {
        private readonly MuetongContext _dbContext;
        public StoreRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Get
        public async Task<bool> AddAsync(Store store)
        {
            await _dbContext.Stores.AddAsync(store);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
