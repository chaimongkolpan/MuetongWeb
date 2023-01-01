using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            PoDetails = new HashSet<PoDetail>();
            PrDetails = new HashSet<PrDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<PoDetail> PoDetails { get; set; }
        public virtual ICollection<PrDetail> PrDetails { get; set; }
    }
}
