using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface ISettingConstantRepositories
    {
        Task<IEnumerable<SettingConstant>> GetAsync(string type);
        Task<IEnumerable<SettingConstant>> GetAsync();
        Task<bool> AddAsync(SettingConstant setting);
        Task<bool> DeleteAsync(long id);
    }
}
