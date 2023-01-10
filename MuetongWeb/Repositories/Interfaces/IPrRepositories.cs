using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPrRepositories
    {
        Task<IEnumerable<Pr>> GetAsync();
        Task<IEnumerable<Pr>> GetByProjectAsync(long projectId);
    }
}
