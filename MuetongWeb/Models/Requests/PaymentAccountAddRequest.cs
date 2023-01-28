namespace MuetongWeb.Models.Requests
{
    public class PaymentAccountAddRequest
    {
        public long StoreId { get; set; }
        public string AccountNo { get; set; } = null!;
        public string? AccountName { get; set; }
        public string? Bank { get; set; }
        public string? Type { get; set; }
    }
}
