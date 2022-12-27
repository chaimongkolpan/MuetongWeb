using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Responses;
namespace MuetongWeb.Models.Pages
{
    public class RoleModel : PageModel
    {
        public bool CanEdit { get; set; } = false;
        public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
        public List<PermissionResponse> Permissions { get; set; } = new List<PermissionResponse>();
        public RoleModel() { }
        public RoleModel(IEnumerable<Role> roles, IEnumerable<Permission> permissions, bool editPermit = false)
        {
            foreach (var role in roles)
                Roles.Add(new RoleResponse(role));
            foreach (var permission in permissions)
                Permissions.Add(new PermissionResponse(permission));
            CanEdit = editPermit;
        }
    }
}
