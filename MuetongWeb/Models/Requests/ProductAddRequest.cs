using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class ProductAddRequest
    {
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public UserInfoModel? User { get; set; }
    }
}
