namespace MuetongWeb.Models.Requests
{
    public class BillingCancelRequest
    {
        public string Remark { get; set; } = string.Empty;
        public BillingCancelRequest() { }
        public BillingCancelRequest(string? remark)
        {
            Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark;
        }
    }
}
