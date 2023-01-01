using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface ISettingServices
    {
        Task<SettingResponse> ImportCustomerFileAsync(SettingImportCustomerRequest request, long userId);
        Task<SettingResponse> ImportStoreFileAsync(SettingImportStoreRequest request, long userId);
    }
}
