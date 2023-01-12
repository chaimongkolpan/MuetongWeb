using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPoRepositories
    {
        Task<IEnumerable<Po>> GetAsync();
        Task<IEnumerable<Po>> GetByProjectAsync(long projectId);
    }
}
