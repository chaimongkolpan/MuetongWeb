namespace MuetongWeb.Models.Requests
{
    public class BillingIndexUpdateRequest
    {
        public string? BillingNo { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public long? PaymentType { get; set; }
        public long? PaymentAccount { get; set; }
        public decimal? Amount { get; set; }
        public string? Remark { get; set; }
        public bool? HasReceipt { get; set; }
        public string? ReceiptNo { get; set; }
        public bool? HasExtra { get; set; }
        public long? ExtraType { get; set; }
        public string? ExtraOther { get; set; }
        public decimal? ExtraAmount { get; set; }
        public bool? HasInvoice { get; set; }
        public string? InvoiceNo { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
