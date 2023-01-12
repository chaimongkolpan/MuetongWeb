using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Pr
    {
        public Pr()
        {
            PrDetails = new HashSet<PrDetail>();
        }

        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string PrNo { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Remark { get; set; }
        public long? ApproverId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproveRemark { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool? IsAdvancePay { get; set; }
        public long? ContractorId { get; set; }
        public bool? IsReadCancel { get; set; }

        public virtual User? Approver { get; set; }
        public virtual Contractor? Contractor { get; set; }
        public virtual Project Project { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<PrDetail> PrDetails { get; set; }
    }
}
