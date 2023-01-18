using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IPoServices
    {
        Task<PoModel> IndexAsync(bool canEdit, UserInfoModel user);
        Task<PoModel> ApproverAsync(bool canEdit, UserInfoModel user);

        Task<bool> IndexAddAsync(PoIndexAddRequest request);
        Task<PoIndexResponse> IndexSearchAsync(PoIndexSearchRequest request);
        Task<bool> UpdateIndexPo(long id, long userId, PoIndexUpdateRequest request);
        Task<bool> Read(long id);
        Task<bool> Cancel(long id, string? remark);
        Task<bool> Approve(long id, long userId);
        Task<PoIndexPrResponse> IndexSearchPrAsync(PoIndexPrSearch request);
        Task<List<UserResponse>> GetRequesterByProject(long projectId);
        Task<List<string>> GetPrNoByProject(long projectId);
        Task<List<string>> GetPoNoByProject(long projectId);
        Task<StoreCollectionResponse> GetStore();
        Task<ReceiveSettingConstantResponse> GetReceive();
        Task<TypeSettingConstantResponse> GetTypeAsync();
    }
}
