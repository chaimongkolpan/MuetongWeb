using MuetongWeb.Constants;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Services.Interfaces
{
    public interface IFileServices
    {
        Task<SettingImportCustomerDataModel?> ReadExcel(SettingImportCustomerRequest request, List<ExcelDataSchema> schemas);
        Task<SettingImportStoreDataModel?> ReadExcel(SettingImportStoreRequest request, List<ExcelDataSchema> schemas);
    }
}
