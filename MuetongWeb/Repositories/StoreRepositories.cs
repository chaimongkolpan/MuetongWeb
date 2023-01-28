using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories
{
    public class StoreRepositories : IStoreRepositories
    {
        private readonly MuetongContext _dbContext;
        public StoreRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Store>> GetAsync(StoreRequest request)
        {
            return await _dbContext.Stores.Where(store => string.IsNullOrWhiteSpace(request.Query)
                                            || (store.Name ?? "").Contains(request.Query)
                                            || (store.Address ?? "").Contains(request.Query)
                                            || (store.PhoneNo ?? "").Contains(request.Query)
                                            || (store.TaxNo ?? "").Contains(request.Query)
                                            || (store.ContractName ?? "").Contains(request.Query)
                                            || (store.Email ?? "").Contains(request.Query)
                                          )
                                          .OrderBy(store => store.Name)
                                          //.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
                                          .Include(store => store.Province)
                                          .Include(store => store.Billings)
                                          .Include(store => store.Pos)
                                          .Include(store => store.PaymentAccounts)
                                          .ToListAsync();
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
        public async Task<bool> UpdateAsync(Store store)
        {
            var tmp = await _dbContext.Stores.FindAsync(store.Id);
            if (tmp == null)
                return false;
            tmp.Name = store.Name;
            tmp.Address = store.Address;
            tmp.ProvinceId = store.ProvinceId;
            tmp.PhoneNo = store.PhoneNo;
            tmp.TaxNo = store.TaxNo;
            tmp.ContractName = store.ContractName;
            tmp.Email = store.Email;
            tmp.ModifyDate = DateTime.Now;
            _dbContext.Stores.Update(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.Stores.FindAsync(id);
            if (tmp == null)
                return false;
            var payaccounts = await _dbContext.PaymentAccounts.Where(pa => pa.StoreId == id).ToListAsync();
            if (payaccounts.Any())
                _dbContext.PaymentAccounts.RemoveRange(payaccounts);
            await _dbContext.SaveChangesAsync();
            _dbContext.Stores.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
