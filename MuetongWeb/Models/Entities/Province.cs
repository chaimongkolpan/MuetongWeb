using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Province
    {
        public Province()
        {
            Contractors = new HashSet<Contractor>();
            Customers = new HashSet<Customer>();
            Projects = new HashSet<Project>();
            Stores = new HashSet<Store>();
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string NameTh { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
