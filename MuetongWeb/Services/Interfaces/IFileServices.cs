using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Services.Interfaces
{
    public interface IFileServices
    {
        Task<MemoryStream?> GetFileAsync(long id);
        Task<bool> DeleteFileAsync(long id);
        Task<List<Models.Entities.File>> GetFilesAsync(long id, string type);
        Task<List<Models.Entities.File>> GetFilesListAsync(List<long> ids, string type);
        Task<bool> ImportFileList(long id, string type, List<IFormFile> files);
        Task<SettingImportCustomerDataModel?> ReadExcel(SettingImportCustomerRequest request, List<ExcelDataSchema> schemas);
        Task<SettingImportStoreDataModel?> ReadExcel(SettingImportStoreRequest request, List<ExcelDataSchema> schemas);
        Task<List<ProjectCode>> ImportProjectCodeExcel(ProjectCodeImportRequest request, long projectId);
    }
}
