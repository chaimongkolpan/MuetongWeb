using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class PoIndexUpdateRequest
    {
        public long StoreId { get; set; }
        public long? CreditType { get; set; }
        public int? DateValue { get; set; }
        public DateTime? DateSpecific { get; set; }
        public string? BgContractNo { get; set; }
        public string? ChequeNo { get; set; }
        public long? PaymentType { get; set; }
        public long? PaymentAccountId { get; set; }
        public DateTime? PayDate { get; set; }
        public long? ReceiveBillingTypeId { get; set; }
        public long? ReceiveReceiptTypeId { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public bool? HasAdditional { get; set; }
        public string? AdditionalType { get; set; }
        public string? AdditionalOther { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? JsonDetails { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? WhtRate { get; set; }
        public List<PoDetailUpdateRequest>? Details { get; set; }
        public string? JsonOther { get; set; }
        public PoDetailUpdateRequest? Other { get; set; }
        public UserInfoModel? User { get; set; }
    }
    public class PoDetailUpdateRequest
    {
        public long? Id { get; set; }
        public long? PrDetailId { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsVat { get; set; }
        public bool? IsWht { get; set; }
    }
}
