using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? BranchNo { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Province? Province { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Project> Projects { get; set; }
    }
}
