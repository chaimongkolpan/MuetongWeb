using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class ProvinceRepositories : IProvinceRepositories
    {
        private readonly MuetongContext _dbContext;
        public ProvinceRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Province>> GetAsync()
        {
            return await _dbContext.Provinces.ToListAsync();
        }
        public async Task<Province?> GetAsync(long id)
        {
            return await _dbContext.Provinces.FindAsync(id);
        }
    }
}
