using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class FileRepositories : IFileRepositories
    {
        private readonly MuetongContext _dbContext;
        public FileRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddAsync(MuetongWeb.Models.Entities.File file)
        {
            await _dbContext.Files.AddAsync(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddRangeAsync(List<MuetongWeb.Models.Entities.File> files)
        {
            await _dbContext.Files.AddRangeAsync(files);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
