using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class PrDetail
    {
        public PrDetail()
        {
            PrReceives = new HashSet<PrReceive>();
        }

        public long Id { get; set; }
        public long PrId { get; set; }
        public long? ProductId { get; set; }
        public long? ProjectCodeId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? UseDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal? PricePerUnit { get; set; }
        public string? Remark { get; set; }
        public long? PoDetailId { get; set; }

        public virtual PoDetail? PoDetail { get; set; }
        public virtual Pr Pr { get; set; } = null!;
        public virtual Product? Product { get; set; }
        public virtual ProjectCode? ProjectCode { get; set; }
        public virtual ICollection<PrReceive> PrReceives { get; set; }
    }
}
