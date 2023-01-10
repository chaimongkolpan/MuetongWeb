using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IPrServices
    {
        Task<PrModel> IndexAsync(bool canEdit, UserInfoModel user);
        Task<PrIndexResponse> IndexSearchAsync(bool canEdit, PrIndexSearchRequest request);

        Task<List<UserResponse>> GetRequesterByProject(long projectId);
        Task<List<string>> GetPrNoByProject(long projectId);
        Task<List<ContractorResponse>> GetContractorByProject(long projectId);
        Task<List<ProductResponse>> GetProduct();
        Task<List<ProjectCodeResponse>> GetProjectCode(long projectId);
    }
}
