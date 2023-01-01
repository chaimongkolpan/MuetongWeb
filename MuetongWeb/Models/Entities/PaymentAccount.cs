using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class PaymentAccount
    {
        public PaymentAccount()
        {
            Billings = new HashSet<Billing>();
            Pos = new HashSet<Po>();
        }

        public long Id { get; set; }
        public long StoreId { get; set; }
        public string AccountNo { get; set; } = null!;
        public string? AccountName { get; set; }
        public string? Bank { get; set; }
        public string? Type { get; set; }

        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<Po> Pos { get; set; }
    }
}
