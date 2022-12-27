using Microsoft.EntityFrameworkCore;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class RoleRepositories : IRoleRepositories
    {
        private readonly MuetongContext _dbContext;
        public RoleRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Role>> GetAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }
        public async Task<Role?> GetAsync(long id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(role => role.Name == name);
        }
        public async Task<bool> AddAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Role role)
        {
            var tmp = await _dbContext.Roles.FindAsync(role.Id);
            if (tmp == null)
                return false;
            tmp.Name = role.Name;
            tmp.HomePageUrl = role.HomePageUrl;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
                return false;
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
