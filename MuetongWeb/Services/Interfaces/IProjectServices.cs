using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IProjectServices
    {
        Task<ProjectCollectionResponse> GetAsync();
        Task<bool> AddAsync(ProjectAddRequest request);
        Task<bool> UpdateAsync(long id, ProjectUpdateRequest request);
        Task<bool> DeleteAsync(long id);

        Task<ProjectResponse> ImportCode(long projectId, ProjectCodeImportRequest request);

        Task<List<UserResponse>> GetUserAsync();
        Task<ProjectResponse> AddUser(long projectId, long id);
        Task<ProjectResponse> DeleteUser(long projectId, long id);

        Task<List<ContractorResponse>> GetContractorAsync();
        Task<ProjectResponse> AddContractor(long projectId, long id);
        Task<ProjectResponse> DeleteContractor(long projectId, long id);

        Task<IEnumerable<Province>> GetProvince();
        Task<List<CustomerResponse>> GetCustomer();
    }
}
