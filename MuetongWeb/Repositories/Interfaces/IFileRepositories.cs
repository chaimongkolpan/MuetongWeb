namespace MuetongWeb.Repositories.Interfaces
{
    public interface IFileRepositories
    {
        Task<bool> AddAsync(MuetongWeb.Models.Entities.File file);
        Task<bool> AddRangeAsync(List<MuetongWeb.Models.Entities.File> files);
    }
}
