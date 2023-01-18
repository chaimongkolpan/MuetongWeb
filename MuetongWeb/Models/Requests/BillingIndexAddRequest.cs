using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Models.Requests
{
    public class BillingIndexAddRequest
    {
        public string? BillingNo { get; set; }
        public DateTime? BillingDate { get; set; }
        public string? JsonDetails { get; set; }
        public List<IFormFile>? Files { get; set; }
        public List<PoResponse>? Pos { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
