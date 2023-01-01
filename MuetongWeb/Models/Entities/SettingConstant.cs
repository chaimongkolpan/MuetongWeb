using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class SettingConstant
    {
        public SettingConstant()
        {
            Billings = new HashSet<Billing>();
            PoBillingReceiveTypeNavigations = new HashSet<Po>();
            PoCreditTypeNavigations = new HashSet<Po>();
            PoPaymentTypeNavigations = new HashSet<Po>();
            PoReceiptReceiveTypeNavigations = new HashSet<Po>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Detail { get; set; }
        public string Type { get; set; } = null!;
        public int OrderNumber { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<Po> PoBillingReceiveTypeNavigations { get; set; }
        public virtual ICollection<Po> PoCreditTypeNavigations { get; set; }
        public virtual ICollection<Po> PoPaymentTypeNavigations { get; set; }
        public virtual ICollection<Po> PoReceiptReceiveTypeNavigations { get; set; }
    }
}
