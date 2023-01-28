using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IContractorRepositories
    {
        Task<IEnumerable<Contractor>> GetAsync();
        Task<IEnumerable<Contractor>> GetAsync(string? query, long? provinceId, int page, int pageSize);
        Task<int> CountAsync(string? query, long? provinceId);
        Task<Contractor?> GetAsync(long id);
        Task<bool> AddAsync(Contractor contractor);
        Task<bool> UpdateAsync(Contractor contractor);
        Task<bool> DeleteAsync(long id);
    }
}
