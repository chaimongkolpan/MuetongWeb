namespace MuetongWeb.Models.Requests
{
    public class StoreUpdateRequest
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? TaxNo { get; set; }
        public string? ContractName { get; set; }
        public string? Email { get; set; }
    }
}
