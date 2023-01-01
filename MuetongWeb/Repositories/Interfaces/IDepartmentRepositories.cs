using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IDepartmentRepositories
    {
        Task<IEnumerable<Department>> GetByLineAsync(long lineId);
        Task<Department?> GetAsync(long id);
        Task<bool> AddAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(long id);
    }
}
