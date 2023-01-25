using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class BillingIndexSearch
    {
        public long ProjectId { get; set; }
        public string? PrNo { get; set; }
        public string? PoNo { get; set; }
        public string? BillingNo { get; set; }
        public long RequesterId { get; set; }
        public string? Status { get; set; }
        public long NoPayment { get; set; }
        public long NoReceipt { get; set; }
        public long NoInvoice { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
