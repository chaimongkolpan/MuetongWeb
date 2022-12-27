using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IRolePermissionRepositories
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<IEnumerable<Permission>> GetPermissionsAsync(long roleId);
        Task<bool> AddAsync(List<RolePermission> rolePermissions);
        Task<bool> DeleteAsync(long roleId);
        Task<bool> UpdateAsync(long roleId, List<RolePermission> rolePermissions);
    }
}
