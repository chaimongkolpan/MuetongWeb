using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Po
    {
        public Po()
        {
            PoBillings = new HashSet<PoBilling>();
            PoDetails = new HashSet<PoDetail>();
        }

        public long Id { get; set; }
        public string PoNo { get; set; } = null!;
        public long StoreId { get; set; }
        public string? GuaranteeStore { get; set; }
        public string? GuaranteeStoreNo { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Total { get; set; }
        public decimal? WhtRate { get; set; }
        public decimal? Wht { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GrandTotal { get; set; }
        public long CreditType { get; set; }
        public int? DateValue { get; set; }
        public DateTime? DateSpecific { get; set; }
        public string? BgContractNo { get; set; }
        public string? ChequeNo { get; set; }
        public long PaymentType { get; set; }
        public long? PaymentAccountId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public long? BillingReceiveType { get; set; }
        public long? ReceiptReceiveType { get; set; }
        public string Status { get; set; } = null!;
        public string? Remark { get; set; }
        public long? ApproverId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproveRemark { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool? IsReadCancel { get; set; }

        public virtual User? Approver { get; set; }
        public virtual SettingConstant? BillingReceiveTypeNavigation { get; set; }
        public virtual SettingConstant CreditTypeNavigation { get; set; } = null!;
        public virtual PaymentAccount? PaymentAccount { get; set; }
        public virtual SettingConstant PaymentTypeNavigation { get; set; } = null!;
        public virtual SettingConstant? ReceiptReceiveTypeNavigation { get; set; }
        public virtual Store Store { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<PoBilling> PoBillings { get; set; }
        public virtual ICollection<PoDetail> PoDetails { get; set; }
    }
}
