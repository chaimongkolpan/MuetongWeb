using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IStoreRepositories
    {
        Task<IEnumerable<Store>> GetAsync(StoreRequest request);
        Task<IEnumerable<Store>> GetAsync();
        Task<Store?> GetAsync(long id);
        Task<bool> AddAsync(Store store);
        Task<bool> UpdateAsync(Store store);
        Task<bool> DeleteAsync(long id);
    }
}
