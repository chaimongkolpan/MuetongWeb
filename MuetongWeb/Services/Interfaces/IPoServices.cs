using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IPoServices
    {
        Task<PoModel> IndexAsync(bool canEdit, UserInfoModel user);
        Task<PoModel> ApproverAsync(bool canEdit, UserInfoModel user);

        Task<PoIndexResponse> IndexSearchAsync(PoIndexSearchRequest request);
        Task<PoIndexPrResponse> IndexSearchPrAsync(PoIndexPrSearch request);
        Task<List<UserResponse>> GetRequesterByProject(long projectId);
        Task<List<string>> GetPrNoByProject(long projectId);
        Task<List<string>> GetPoNoByProject(long projectId);
    }
}
