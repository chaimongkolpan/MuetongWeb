using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IBillingServices
    {
        Task<BillingModel> IndexAsync(bool canEdit, UserInfoModel user);
        Task<BillingModel> ApproverAsync(bool canEdit, UserInfoModel user);

        Task<bool> AddAsync(BillingIndexAddRequest request);
        Task<bool> UpdateAsync(long id, BillingIndexUpdateRequest request);
        Task<bool> SendApproveAsync(long id);
        Task<BillingIndexResponse> Search(BillingIndexSearch request);
        Task<BillingIndexPoResponse> SearchPo(BillingIndexPoSearch request);

        Task<List<UserResponse>> GetRequesterByProject(long projectId);
        Task<List<string>> GetPrNoByProject(long projectId);
        Task<List<string>> GetPoNoByProject(long projectId);
        Task<List<string>> GetBillingNoByProject(long projectId);
        Task<bool> ApproveAsync(long id, BillingApproveRequest request);
        Task<bool> ReadAsync(long id);
        Task<bool> CancelAsync(long id, BillingCancelRequest request);
        Task<BillPaymentResponse> GetPaymentByBill(long id);
        Task<FileModalResponse> GetFiles(long id, string type);
    }
}
