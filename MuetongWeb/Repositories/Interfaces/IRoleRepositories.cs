using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories.Interfaces
{
    public interface IRoleRepositories
    {
        Task<IEnumerable<Role>> GetAsync();
        Task<Role?> GetAsync(long id);
        Task<Role?> GetByNameAsync(string name);
        Task<bool> AddAsync(Role role);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(long id);
    }
}
