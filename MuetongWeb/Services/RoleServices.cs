using MuetongWeb.Services.Interfaces;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Responses;
using Newtonsoft.Json;
using MuetongWeb.Mappers;

namespace MuetongWeb.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly ILogger<RoleServices> _logger;
        private readonly IRolePermissionRepositories _rolePermissionRepositories;
        private readonly IRoleRepositories _roleRepositories;
        public RoleServices
        (
            ILogger<RoleServices> logger,
            IRolePermissionRepositories rolePermissionRepositories,
            IRoleRepositories roleRepositories
        )
        {
            _logger = logger;
            _rolePermissionRepositories = rolePermissionRepositories;
            _roleRepositories = roleRepositories;
        }

        public async Task<RoleModel> GetRole(bool editPermit = false)
        {
            try
            {
                var roles = await _roleRepositories.GetAsync();
                var permissions = await _rolePermissionRepositories.GetPermissionsAsync();
                return new RoleModel(roles, permissions, editPermit);
            }
            catch(Exception ex)
            {
                _logger.LogError("RoleServices => GetRole: " + ex.Message);
                throw;
            }
        }
        public async Task<RoleResponse?> GetRole(long id)
        {
            try
            {
                var role = await _roleRepositories.GetAsync(id);
                if (role == null)
                    return null;
                var response = new RoleResponse(role);
                var permissions = await _rolePermissionRepositories.GetPermissionsAsync(id);
                if (permissions.Any())
                    response.SetPermissions(permissions.Select(permission => permission.Id).ToArray());
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleServices => GetRole: id=" + id + " with error= " + ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdateAsync(RoleUpdateRequest request)
        {
            try
            {
                var role = await _roleRepositories.GetAsync(request.Id);
                if (role == null)
                    return false;
                role = RoleMappers.UpdateRequestMapper(role, request);
                if (await _roleRepositories.UpdateAsync(role))
                {
                    List<RolePermission> rolePermissions = new List<RolePermission>();
                    if (request.Permissions != null && request.Permissions.Any())
                    {
                        foreach (var permission in request.Permissions.ToList())
                        {
                            rolePermissions.Add(new RolePermission()
                            {
                                RoleId = request.Id,
                                PermissionId = permission
                            });
                        }
                    }
                    if (await _rolePermissionRepositories.UpdateAsync(request.Id, rolePermissions))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleServices => UpdateAsync: request=" + JsonConvert.SerializeObject(request) + " with error= " + ex.Message);
                throw;
            }
        }
    }
}
