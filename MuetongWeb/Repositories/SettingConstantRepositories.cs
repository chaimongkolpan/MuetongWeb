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
    }
}
