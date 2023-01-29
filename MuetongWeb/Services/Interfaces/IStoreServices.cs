using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IStoreServices
    {
        Task<StoreCollectionResponse> GetAsync(StoreRequest request);
        Task<bool> AddAsync(StoreAddRequest request);
        Task<bool> UpdateAsync(long id, StoreUpdateRequest request);
        Task<bool> DeleteAsync(long id);
        Task<long> AddAccountAsync(PaymentAccountAddRequest request);
        Task<bool> UpdateAccountAsync(long id, PaymentAccountUpdateRequest request);
        Task<bool> DeleteAccountAsync(long id);
        Task<IEnumerable<Province>> GetProvince();
    }
}
