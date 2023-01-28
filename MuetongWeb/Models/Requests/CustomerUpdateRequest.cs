namespace MuetongWeb.Models.Requests
{
    public class CustomerUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? BranchNo { get; set; }
    }
}
