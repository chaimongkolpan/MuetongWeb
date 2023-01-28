using MuetongWeb.Models.Entities;
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
        public static string WorkLineName(SubDepartment subDepartment)
        {
            var response = string.Empty;
            if (subDepartment != null)
            {
                response = subDepartment.Name;
                if (subDepartment.Department != null)
                {
                    response = response + " : " + subDepartment.Department.Name;
                    if (subDepartment.Department.Line != null)
                        response = response + " : " + subDepartment.Department.Line.Name;
                }
            }
            return response;
        }
    }
}
