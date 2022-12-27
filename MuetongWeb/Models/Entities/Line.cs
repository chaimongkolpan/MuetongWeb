using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Line
    {
        public Line()
        {
            Departments = new HashSet<Department>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Department> Departments { get; set; }
    }
}
