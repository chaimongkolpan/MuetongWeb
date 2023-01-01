using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface ISubDepartmentRepositories
    {
        Task<IEnumerable<SubDepartment>> GetByDepartmentAsync(long departmentId);
        Task<SubDepartment?> GetAsync(long id);
        Task<bool> AddAsync(SubDepartment subDepartment);
        Task<bool> UpdateAsync(SubDepartment subDepartment);
        Task<bool> DeleteAsync(long id);
    }
}
