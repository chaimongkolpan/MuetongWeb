using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Billings = new HashSet<Billing>();
            Contractors = new HashSet<Contractor>();
            Customers = new HashSet<Customer>();
            Departments = new HashSet<Department>();
            InverseUserNavigation = new HashSet<User>();
            Lines = new HashSet<Line>();
            PoApprovers = new HashSet<Po>();
            PoUsers = new HashSet<Po>();
            PrApprovers = new HashSet<Pr>();
            PrReceives = new HashSet<PrReceive>();
            PrUsers = new HashSet<Pr>();
            Products = new HashSet<Product>();
            ProjectUsers = new HashSet<ProjectUser>();
            Projects = new HashSet<Project>();
            Stores = new HashSet<Store>();
            SubDepartments = new HashSet<SubDepartment>();
        }

        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public long SubDepartmentId { get; set; }
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? EmployeeId { get; set; }
        public string? CitizenId { get; set; }

        public virtual Province? Province { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual SubDepartment SubDepartment { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<User> InverseUserNavigation { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
        public virtual ICollection<Po> PoApprovers { get; set; }
        public virtual ICollection<Po> PoUsers { get; set; }
        public virtual ICollection<Pr> PrApprovers { get; set; }
        public virtual ICollection<PrReceive> PrReceives { get; set; }
        public virtual ICollection<Pr> PrUsers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<SubDepartment> SubDepartments { get; set; }
    }
}
