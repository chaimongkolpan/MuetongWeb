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
        public async Task<Models.Entities.File?> GetAsync(long id)
        {
            return await _dbContext.Files.FindAsync(id);
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var file = await _dbContext.Files.FindAsync(id);
            if (file == null)
                return false;
            _dbContext.Files.Remove(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteListAsync(long entityId, string type)
        {
            var files = await _dbContext.Files.Where(file => file.EntityId == entityId && file.Type == type).ToListAsync();
            if (!files.Any())
                return false;
            _dbContext.Files.RemoveRange(files);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Models.Entities.File>> GetAsync(long entityId, string type)
        {
            var files = await _dbContext.Files.Where(file => file.EntityId == entityId && file.Type == type).ToListAsync();
            return files;
        }
        public async Task<List<Models.Entities.File>> GetAsync(List<long> entityIds, string type)
        {
            var files = await _dbContext.Files.Where(file => entityIds.Contains(file.EntityId) && file.Type == type).ToListAsync();
            return files;
        }
        public async Task<bool> AddAsync(Models.Entities.File file)
        {
            await _dbContext.Files.AddAsync(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddRangeAsync(List<Models.Entities.File> files)
        {
            await _dbContext.Files.AddRangeAsync(files);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
