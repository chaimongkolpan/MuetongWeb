using Microsoft.EntityFrameworkCore;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class RolePermissionRepositories : IRolePermissionRepositories
    {
        private readonly MuetongContext _dbContext;
        public RolePermissionRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _dbContext.Permissions.ToListAsync();
        }
        public async Task<IEnumerable<Permission>> GetPermissionsAsync(long roleId)
        {
            var rolePermissions = await _dbContext.RolePermissions.Where(rolePermission => rolePermission.RoleId == roleId)
                                                    .Include(rolePermission => rolePermission.Permission)
                                                    .ToListAsync();
            if (rolePermissions.Any())
                return rolePermissions.Select(rolePermission => rolePermission.Permission).ToList();
            return new List<Permission>();
        }
        public async Task<bool> AddAsync(List<RolePermission> rolePermissions)
        {
            await _dbContext.RolePermissions.AddRangeAsync(rolePermissions);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long roleId)
        {
            var rolePermissions = await _dbContext.RolePermissions.Where(rolePermission => rolePermission.RoleId == roleId)
                                                    .ToListAsync();
            if (rolePermissions.Any())
            {
                _dbContext.RolePermissions.RemoveRange(rolePermissions);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(long roleId, List<RolePermission> rolePermissions)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var oldRolePermissions = await _dbContext.RolePermissions.Where(rolePermission => rolePermission.RoleId == roleId)
                                                    .ToListAsync();
                if (oldRolePermissions.Any())
                {
                    _dbContext.RolePermissions.RemoveRange(oldRolePermissions);
                    await _dbContext.SaveChangesAsync();
                }
                await _dbContext.RolePermissions.AddRangeAsync(rolePermissions);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
