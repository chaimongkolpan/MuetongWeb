using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Department
    {
        public Department()
        {
            SubDepartments = new HashSet<SubDepartment>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long LineId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Line Line { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<SubDepartment> SubDepartments { get; set; }
    }
}
