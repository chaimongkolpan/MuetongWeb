using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Contractor
    {
        public Contractor()
        {
            ProjectContractors = new HashSet<ProjectContractor>();
            Prs = new HashSet<Pr>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? Type { get; set; }
        public string? DirectorName { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Province? Province { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProjectContractor> ProjectContractors { get; set; }
        public virtual ICollection<Pr> Prs { get; set; }
    }
}
