using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class ProjectContractor
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long ContractorId { get; set; }

        public virtual Contractor Contractor { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
