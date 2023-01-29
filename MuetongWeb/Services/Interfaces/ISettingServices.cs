using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface ISettingServices
    {
        Task<SettingCollectionResponse> GetAll();
        Task<bool> Add(string name, string type);
        Task<bool> Delete(long id);
        Task<SettingResponse> ImportCustomerFileAsync(SettingImportCustomerRequest request, long userId);
        Task<SettingResponse> ImportStoreFileAsync(SettingImportStoreRequest request, long userId);
    }
}
