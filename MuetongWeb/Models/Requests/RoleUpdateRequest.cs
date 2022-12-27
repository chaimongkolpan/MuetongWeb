namespace MuetongWeb.Models.Requests
{
    public class RoleUpdateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? HomePageUrl { get; set; }
        public long[]? Permissions { get; set; }
    }
}
