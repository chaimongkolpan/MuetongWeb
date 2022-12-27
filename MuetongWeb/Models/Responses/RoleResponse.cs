using MuetongWeb.Models.Entities;
namespace MuetongWeb.Models.Responses
{
    public class RoleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? HomePageUrl { get; set; }
        public long[]? Permissions { get; set; }
        public RoleResponse() { }
        public RoleResponse(Role role)
        {
            Id = role.Id;
            Name = role.Name;
            HomePageUrl = role.HomePageUrl;
        }
        public void SetPermissions(long[] permissions)
        {
            Permissions = permissions;
        }
    }
}
