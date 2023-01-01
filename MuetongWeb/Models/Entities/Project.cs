using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Project
    {
        public Project()
        {
            ProjectCodes = new HashSet<ProjectCode>();
            ProjectContractors = new HashSet<ProjectContractor>();
            ProjectUsers = new HashSet<ProjectUser>();
            Prs = new HashSet<Pr>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContractNo { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Province? Province { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProjectCode> ProjectCodes { get; set; }
        public virtual ICollection<ProjectContractor> ProjectContractors { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<Pr> Prs { get; set; }
    }
}
