using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IPrServices
    {
        Task<PrModel> IndexAsync(bool canEdit, UserInfoModel user);
        Task<PrModel> ApproverAsync(bool canEdit, UserInfoModel user);
        Task<PrModel> ReceiveAsync(bool canEdit, UserInfoModel user);

        Task<PrIndexResponse> IndexSearchAsync(PrIndexSearchRequest request);
        Task<PrResponse?> Get(long id);
        Task<bool> Read(long id);
        Task<bool> Cancel(long id);
        Task<bool> Approve(long id, long userId, PrApproveRequest request);
        Task<bool> IndexAddAsync(long userId, PrIndexAddRequest request);
        Task<bool> IndexUpdateAsync(long id, long userId, PrIndexUpdateRequest request);
        Task<List<UserResponse>> GetRequesterByProject(long projectId);
        Task<List<string>> GetPrNoByProject(long projectId);
        Task<List<ContractorResponse>> GetContractorByProject(long projectId);
        Task<List<ProductResponse>> GetProduct();
        Task<List<ProjectCodeResponse>> GetProjectCode(long projectId);
        Task<FileModalResponse> GetFiles(long id, string type);

        Task<PrReceiveResponse> ReceiveSearchAsync(PrReceiveSearchRequest request);
        Task<bool> ReceiveListAsync(PrReceiveAddRequest request);
        Task<bool> ReceiveAsync(PrReceiveDetailRequest request);
    }
}
