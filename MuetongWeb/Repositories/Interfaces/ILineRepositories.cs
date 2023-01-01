using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface ILineRepositories
    {
        Task<IEnumerable<Line>> GetAsync();
        Task<Line?> GetAsync(long id);
        Task<bool> AddAsync(Line line);
        Task<bool> UpdateAsync(Line line);
        Task<bool> DeleteAsync(long id);
    }
}
