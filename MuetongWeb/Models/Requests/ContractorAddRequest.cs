using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class ContractorAddRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? Type { get; set; }
        public string? DirectorName { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
