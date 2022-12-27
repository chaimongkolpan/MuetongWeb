using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class SubDepartment
    {
        public SubDepartment()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long DepartmentId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
