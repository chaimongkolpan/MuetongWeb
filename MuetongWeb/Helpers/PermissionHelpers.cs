using MuetongWeb.Models.Pages;

namespace MuetongWeb.Helpers
{
    public static class PermissionHelpers
    {
        public static bool Authenticate(string permissionName, List<PermissionModel> permissions)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
                return false;
            return permissions.Any(permission => permission.Name == permissionName);
        }
    }
}
