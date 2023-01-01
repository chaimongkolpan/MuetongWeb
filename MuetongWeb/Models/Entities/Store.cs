using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Store
    {
        public Store()
        {
            Billings = new HashSet<Billing>();
            PaymentAccounts = new HashSet<PaymentAccount>();
            Pos = new HashSet<Po>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? TaxNo { get; set; }
        public string? ContractName { get; set; }
        public string? Email { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Province? Province { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<PaymentAccount> PaymentAccounts { get; set; }
        public virtual ICollection<Po> Pos { get; set; }
    }
}
