namespace MuetongWeb.Models.Requests
{
    public class ProjectUpdateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long? ProvinceId { get; set; }
        public long CustomerId { get; set; }
    }
}
