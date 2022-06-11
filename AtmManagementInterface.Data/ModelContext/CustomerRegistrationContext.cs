using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class CustomerRegistrationContext : DbContext
    {
        public CustomerRegistrationContext()
        {
        }

        public CustomerRegistrationContext(DbContextOptions<CustomerRegistrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
        public virtual DbSet<Lgas> Lgas { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<TblEjfileUploadedDocument> TblEjfileUploadedDocument { get; set; }
        public virtual DbSet<TblEjfileUploadedDocumentTemp> TblEjfileUploadedDocumentTemp { get; set; }
        public virtual DbSet<TblUserInformation> TblUserInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ADEGBOYEGAOLUWA\\GBEN;Database=CustomerRegistration;user id=sa;password=sqluser10$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetails>(entity =>
            {
                entity.HasIndex(e => e.TblStatestateId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsOtpvalidated).HasColumnName("IsOTPValidated");

                entity.Property(e => e.Otp).HasColumnName("OTP");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TblStatestateId).HasColumnName("TBL_STATEStateId");

                entity.HasOne(d => d.TblStatestate)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.TblStatestateId);
            });

            modelBuilder.Entity<Lgas>(entity =>
            {
                entity.HasKey(e => e.Lgaid);

                entity.ToTable("LGAs");

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.Lganame)
                    .IsUnique();

                entity.HasIndex(e => e.StateId);

                entity.Property(e => e.Lgaid).HasColumnName("LGAId");

                entity.Property(e => e.Lganame)
                    .IsRequired()
                    .HasColumnName("LGAName");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Lgas)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Lgas)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.HasIndex(e => e.StateName)
                    .IsUnique();

                entity.Property(e => e.StateName).IsRequired();
            });

            modelBuilder.Entity<TblEjfileUploadedDocument>(entity =>
            {
                entity.ToTable("tbl_EJFileUploadedDocument");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AtmId)
                    .IsRequired()
                    .HasColumnName("Atm_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AvailableAmt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DatePosted).HasColumnType("date");

                entity.Property(e => e.DestinationAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LedgerAmt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Pan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SurCharge).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tns)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEjfileUploadedDocumentTemp>(entity =>
            {
                entity.ToTable("tbl_EJFileUploadedDocumentTemp");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AtmId)
                    .IsRequired()
                    .HasColumnName("Atm_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AvailableAmt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LedgerAmt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Pan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SurCharge).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tns)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUserInformation>(entity =>
            {
                entity.ToTable("tbl_UserInformation");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageData).HasMaxLength(50);

                entity.Property(e => e.ImageTitle)
                    .HasColumnName("imageTitle")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locked)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinGender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PcCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Rank)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RelationShip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
