using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface ISettingConstantRepositories
    {
        Task<IEnumerable<SettingConstant>> GetAsync(string type);
    }
}
