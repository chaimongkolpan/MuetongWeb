namespace MuetongWeb.Repositories.Interfaces
{
    public interface IFileRepositories
    {
        Task<Models.Entities.File?> GetAsync(long id);
        Task<bool> DeleteAsync(long id);
        Task<bool> DeleteListAsync(long entityId, string type);
        Task<List<Models.Entities.File>> GetAsync(long entityId, string type);
        Task<List<Models.Entities.File>> GetAsync(List<long> entityIds, string type);
        Task<bool> AddAsync(MuetongWeb.Models.Entities.File file);
        Task<bool> AddRangeAsync(List<MuetongWeb.Models.Entities.File> files);
    }
}
