using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Billing
    {
        public Billing()
        {
            PoBillings = new HashSet<PoBilling>();
        }

        public long Id { get; set; }
        public long StoreId { get; set; }
        public string BillingNo { get; set; } = null!;
        public DateTime BillingDate { get; set; }
        public string Status { get; set; } = null!;
        public long PaymentType { get; set; }
        public long? PaymentAccountId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string? ReceiptNo { get; set; }
        public string? Remark { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? ApproverId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproveRemark { get; set; }

        public virtual PaymentAccount? PaymentAccount { get; set; }
        public virtual SettingConstant PaymentTypeNavigation { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<PoBilling> PoBillings { get; set; }
    }
}
