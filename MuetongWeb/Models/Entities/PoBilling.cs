using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class PoBilling
    {
        public long Id { get; set; }
        public long PoId { get; set; }
        public long BillingId { get; set; }
        public bool? HasExtra { get; set; }

        public virtual Billing Billing { get; set; } = null!;
        public virtual Po Po { get; set; } = null!;
    }
}
