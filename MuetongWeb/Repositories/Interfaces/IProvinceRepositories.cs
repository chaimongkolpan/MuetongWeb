using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IProvinceRepositories
    {
        Task<IEnumerable<Province>> GetAsync();
        Task<Province?> GetAsync(long id);
    }
}
