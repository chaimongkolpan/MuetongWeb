using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IContractorRepositories
    {
        Task<bool> AddAsync(Contractor contractor);
    }
}
