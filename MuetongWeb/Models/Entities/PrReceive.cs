using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class PrReceive
    {
        public long Id { get; set; }
        public long PrDetailId { get; set; }
        public decimal Quantity { get; set; }
        public string? Remark { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual PrDetail PrDetail { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
