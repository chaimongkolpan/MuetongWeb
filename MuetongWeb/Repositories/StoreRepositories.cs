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
        public async Task<IEnumerable<Store>> GetAsync()
        {
            return await _dbContext.Stores.OrderBy(store => store.Name)
                                          .Include(store => store.PaymentAccounts)
                                          .ToListAsync();
        }
        public async Task<Store?> GetAsync(long id)
        {
            return await _dbContext.Stores.Where(store => store.Id == id)
                                          .Include(store => store.PaymentAccounts)
                                          .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Store store)
        {
            await _dbContext.Stores.AddAsync(store);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
