using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class SettingConstantRepositories : ISettingConstantRepositories
    {
        private readonly MuetongContext _dbContext;
        public SettingConstantRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SettingConstant>> GetAsync(string type)
        {
            return await _dbContext.SettingConstants.Where(setting => setting.Type == type)
                                                    .OrderBy(setting => setting.OrderNumber)
                                                    .ToListAsync();
        }
        public async Task<IEnumerable<SettingConstant>> GetAsync()
        {
            return await _dbContext.SettingConstants.OrderBy(setting => setting.Type).ThenBy(setting => setting.OrderNumber)
                                                    .ToListAsync();
        }
        public async Task<bool> AddAsync(SettingConstant setting)
        {
            var max = _dbContext.SettingConstants.Where(sett => sett.Type == setting.Type).Max(sett => sett.OrderNumber);
            setting.OrderNumber = max + 1;
            await _dbContext.SettingConstants.AddAsync(setting);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.SettingConstants.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.SettingConstants.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
