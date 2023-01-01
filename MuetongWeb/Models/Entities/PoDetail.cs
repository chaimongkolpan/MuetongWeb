using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class PoDetail
    {
        public PoDetail()
        {
            PrDetails = new HashSet<PrDetail>();
        }

        public long Id { get; set; }
        public long PoId { get; set; }
        public long? ProductId { get; set; }
        public string? Code { get; set; }
        public string? ProductCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? Vat { get; set; }
        public decimal? WhtRate { get; set; }
        public decimal? Wht { get; set; }
        public decimal? GrandTotal { get; set; }
        public string? Remark { get; set; }

        public virtual Po Po { get; set; } = null!;
        public virtual Product? Product { get; set; }
        public virtual ICollection<PrDetail> PrDetails { get; set; }
    }
}
