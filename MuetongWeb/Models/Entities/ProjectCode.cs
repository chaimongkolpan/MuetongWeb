using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class ProjectCode
    {
        public ProjectCode()
        {
            InverseParent = new HashSet<ProjectCode>();
            PrDetails = new HashSet<PrDetail>();
        }

        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string Code { get; set; } = null!;
        public string? Detail { get; set; }
        public decimal? Budjet { get; set; }
        public decimal? Cost { get; set; }
        public long? ParentId { get; set; }

        public virtual ProjectCode? Parent { get; set; }
        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<ProjectCode> InverseParent { get; set; }
        public virtual ICollection<PrDetail> PrDetails { get; set; }
    }
}
