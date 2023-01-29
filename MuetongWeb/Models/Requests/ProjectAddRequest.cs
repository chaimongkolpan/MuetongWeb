using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class ProjectAddRequest
    {
        public string Name { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long? ProvinceId { get; set; }
        public long CustomerId { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
