using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IStoreRepositories
    {
        Task<bool> AddAsync(Store store);
    }
}
