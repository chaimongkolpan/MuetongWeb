using MuetongWeb.Models.Entities;
namespace MuetongWeb.Models.Pages
{
    public class UserInfoModel
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? EmployeeId { get; set; }
        public string? HomePageUrl { get; set; }
        public string Role { get; set; } = null!;
        public List<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
        public UserInfoModel() { }
        public UserInfoModel(User user, IEnumerable<Permission> permissions)
        {
            Id = user.Id;
            Username = user.Username;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            EmployeeId = user.EmployeeId;
            HomePageUrl = user.Role.HomePageUrl;
            foreach (var permission in permissions)
            {
                Permissions.Add(new PermissionModel(permission.Name));
            }
            Role = user.Role.Name;
        }
    } 
    public class PermissionModel
    {
        public string Name { get; set; } = null!;
        public PermissionModel(string name)
        {
            Name = name;
        }
    }
}
