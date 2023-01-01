using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories.Interfaces
{
    public interface IProjectRepositories
    {
        Task<IEnumerable<Project>> GetByCustomerAsync(long customerId);
        Task<Project?> GetAsync(long id);
        Task<bool> AddAsync(Project project);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(long id);

        Task<bool> AddUserAsync(List<ProjectUser> projectUsers);
        Task<bool> DeleteUserAsync(long id);

        Task<bool> AddContractorAsync(List<ProjectContractor> projectContractors);
        Task<bool> DeleteContractorAsync(long id);

        Task<IEnumerable<ProjectCode>> GetCodeAsync(long projectId);
        Task<bool> AddCodeAsync(ProjectCode projectCode);
        Task<bool> AddCodeListAsync(List<ProjectCode> projectCodes);
        Task<bool> UpdateCodeAsync(ProjectCode projectCode);
        Task<bool> DeleteCodeAsync(long id);
        Task<bool> DeleteCodeAllAsync(long id);
    }
}
