using MuetongWeb.Models.Entities;
namespace MuetongWeb.Models.Responses
{
    public class PermissionResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsGranted { get; set; } = false;
        public PermissionResponse() { }
        public PermissionResponse(Permission permission)
        {
            Id = permission.Id;
            Name = permission.Name;
            Description = permission.Description;
        }
    }
}
