using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IBillingServices
    {
        Task<bool> AddAsync(BillingIndexAddRequest request);
        Task<bool> UpdateAsync(BillingIndexUpdateRequest request);
        Task<BillingIndexResponse> Search(BillingIndexSearch request);
        Task<BillingIndexPoResponse> SearchPo(BillingIndexPoSearch request);
    }
}
