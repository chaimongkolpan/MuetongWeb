using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IStoreRepositories
    {
        Task<IEnumerable<Store>> GetAsync();
        Task<Store?> GetAsync(long id);
        Task<bool> AddAsync(Store store);
    }
}
