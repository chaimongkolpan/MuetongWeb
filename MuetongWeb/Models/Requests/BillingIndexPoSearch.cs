using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class BillingIndexPoSearch
    {
        public long StoreId { get; set; }
        public string? PoNo { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
