using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MuetongWeb.Models.Entities
{
    public partial class MuetongContext : DbContext
    {
        public MuetongContext()
        {
        }

        public MuetongContext(DbContextOptions<MuetongContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Billing> Billings { get; set; } = null!;
        public virtual DbSet<Contractor> Contractors { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<Line> Lines { get; set; } = null!;
        public virtual DbSet<PaymentAccount> PaymentAccounts { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Po> Pos { get; set; } = null!;
        public virtual DbSet<PoBilling> PoBillings { get; set; } = null!;
        public virtual DbSet<PoDetail> PoDetails { get; set; } = null!;
        public virtual DbSet<Pr> Prs { get; set; } = null!;
        public virtual DbSet<PrDetail> PrDetails { get; set; } = null!;
        public virtual DbSet<PrReceive> PrReceives { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectCode> ProjectCodes { get; set; } = null!;
        public virtual DbSet<ProjectContractor> ProjectContractors { get; set; } = null!;
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public virtual DbSet<SettingConstant> SettingConstants { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<SubDepartment> SubDepartments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        private readonly string? _connectionString;

        public MuetongContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("ConnectionString can't be empty");
            }

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                builder.UseSqlServer(_connectionString);
                base.OnConfiguring(builder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_100_CI_AS_KS_SC_UTF8");

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.ToTable("Billing");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproveRemark).IsUnicode(false);

                entity.Property(e => e.BillingDate).HasColumnType("datetime");

                entity.Property(e => e.BillingNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PaymentAccount)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.PaymentAccountId)
                    .HasConstraintName("FK_Billing_PaymentAccount");

                entity.HasOne(d => d.PaymentTypeNavigation)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.PaymentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Billing_SettingConstant");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Billing_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Billing_User");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.ToTable("Contractor");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Contractors)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Contractor_Province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contractors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contractor_User");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Detail).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Customer_Province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_User");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Line");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_User");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.Extention)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.ToTable("Line");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lines)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Line_User");
            });

            modelBuilder.Entity<PaymentAccount>(entity =>
            {
                entity.ToTable("PaymentAccount");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PaymentAccounts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentAccount_Store");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Po>(entity =>
            {
                entity.ToTable("Po");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproveRemark).IsUnicode(false);

                entity.Property(e => e.BgContractNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateSpecific).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GuaranteeStore)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GuaranteeStoreNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PlanTransferDate).HasColumnType("datetime");

                entity.Property(e => e.PoNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VatRate).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Wht).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WhtRate).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.PoApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_Po_Approve");

                entity.HasOne(d => d.BillingReceiveTypeNavigation)
                    .WithMany(p => p.PoBillingReceiveTypeNavigations)
                    .HasForeignKey(d => d.BillingReceiveType)
                    .HasConstraintName("FK_Po_Billing");

                entity.HasOne(d => d.CreditTypeNavigation)
                    .WithMany(p => p.PoCreditTypeNavigations)
                    .HasForeignKey(d => d.CreditType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Po_CreditType");

                entity.HasOne(d => d.PaymentAccount)
                    .WithMany(p => p.Pos)
                    .HasForeignKey(d => d.PaymentAccountId)
                    .HasConstraintName("FK_Po_PaymentAccount");

                entity.HasOne(d => d.PaymentTypeNavigation)
                    .WithMany(p => p.PoPaymentTypeNavigations)
                    .HasForeignKey(d => d.PaymentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Po_PaymentType");

                entity.HasOne(d => d.ReceiptReceiveTypeNavigation)
                    .WithMany(p => p.PoReceiptReceiveTypeNavigations)
                    .HasForeignKey(d => d.ReceiptReceiveType)
                    .HasConstraintName("FK_Po_Receipt");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Pos)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Po_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PoUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Po_User");
            });

            modelBuilder.Entity<PoBilling>(entity =>
            {
                entity.ToTable("PoBilling");

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.PoBillings)
                    .HasForeignKey(d => d.BillingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PoBilling_Billing");

                entity.HasOne(d => d.Po)
                    .WithMany(p => p.PoBillings)
                    .HasForeignKey(d => d.PoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PoBilling_Po");
            });

            modelBuilder.Entity<PoDetail>(entity =>
            {
                entity.ToTable("PoDetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VatRate).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Wht).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WhtRate).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Po)
                    .WithMany(p => p.PoDetails)
                    .HasForeignKey(d => d.PoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PoDetail_Po");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PoDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PoDetail_Product");
            });

            modelBuilder.Entity<Pr>(entity =>
            {
                entity.ToTable("Pr");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproveRemark).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PrNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.PrApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_Pr_Approver");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.Prs)
                    .HasForeignKey(d => d.ContractorId)
                    .HasConstraintName("FK_Pr_Contractor");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Prs)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pr_Project");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pr_User");
            });

            modelBuilder.Entity<PrDetail>(entity =>
            {
                entity.ToTable("PrDetail");

                entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UseDate).HasColumnType("datetime");

                entity.HasOne(d => d.PoDetail)
                    .WithMany(p => p.PrDetails)
                    .HasForeignKey(d => d.PoDetailId)
                    .HasConstraintName("FK_PrDetail_PoDetail");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.PrDetails)
                    .HasForeignKey(d => d.PrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrDetail_Pr");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PrDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PrDetail_Product");

                entity.HasOne(d => d.ProjectCode)
                    .WithMany(p => p.PrDetails)
                    .HasForeignKey(d => d.ProjectCodeId)
                    .HasConstraintName("FK_PrDetail_ProjectCode");
            });

            modelBuilder.Entity<PrReceive>(entity =>
            {
                entity.ToTable("PrReceive");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.HasOne(d => d.PrDetail)
                    .WithMany(p => p.PrReceives)
                    .HasForeignKey(d => d.PrDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrReceive_PrDetail");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrReceives)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrReceive_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_User");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.ContractNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Customer");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Project_Province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_User");
            });

            modelBuilder.Entity<ProjectCode>(entity =>
            {
                entity.ToTable("ProjectCode");

                entity.Property(e => e.Budjet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Detail).IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ProjectCode_ProjectCode");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCodes)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCode_Project");
            });

            modelBuilder.Entity<ProjectContractor>(entity =>
            {
                entity.ToTable("ProjectContractor");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.ProjectContractors)
                    .HasForeignKey(d => d.ContractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectContractor_Contractor");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectContractors)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectContractor_Project");
            });

            modelBuilder.Entity<ProjectUser>(entity =>
            {
                entity.ToTable("ProjectUser");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUser_Project");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUser_User");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NameTh)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.HomePageUrl).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Role");
            });

            modelBuilder.Entity<SettingConstant>(entity =>
            {
                entity.ToTable("SettingConstant");

                entity.Property(e => e.Detail).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.ContractName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Store_Province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_User");
            });

            modelBuilder.Entity<SubDepartment>(entity =>
            {
                entity.ToTable("SubDepartment");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubDepartment_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubDepartments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubDepartment_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CitizenId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_User_Province");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");

                entity.HasOne(d => d.SubDepartment)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_SubDepartment");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.InverseUserNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
