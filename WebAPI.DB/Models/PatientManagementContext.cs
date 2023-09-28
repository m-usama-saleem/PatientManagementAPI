using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DB.Models;

public partial class PatientManagementContext : DbContext
{
    public PatientManagementContext()
    {
    }

    public PatientManagementContext(DbContextOptions<PatientManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }

    public virtual DbSet<GroupRoute> GroupRoutes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<ProcedureList> ProcedureLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRoute> UserRoutes { get; set; }

    public virtual DbSet<UsersInGroup> UsersInGroups { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=PatientManagement;Trusted_Connection=True;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC076DE7A279");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupPer__3214EC072C6F0ADC");

            entity.Property(e => e.AssignBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AssignDate).HasColumnType("datetime");
            entity.Property(e => e.PermissionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Group).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupPerm__Group__31EC6D26");

            entity.HasOne(d => d.Permission).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupPerm__Permi__32E0915F");
        });

        modelBuilder.Entity<GroupRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupRou__3214EC079EB25382");

            entity.Property(e => e.AssignBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AssignDate).HasColumnType("datetime");
            entity.Property(e => e.RouteId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Group).WithMany(p => p.GroupRoutes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupRout__Group__2E1BDC42");

            entity.HasOne(d => d.Route).WithMany(p => p.GroupRoutes)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupRout__Route__2F10007B");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__3214EC07057ACC3B");

            entity.ToTable("Invoice");

            entity.Property(e => e.AdditionalNotes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NetPayable).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(16, 2)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__DoctorI__5070F446");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Patient__4F7CD00D");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceD__3214EC071CBE5EAA");

            entity.ToTable("InvoiceDetail");

            entity.Property(e => e.Discount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.ProceduralNotes).IsUnicode(false);
            entity.Property(e => e.Qty).HasColumnType("decimal(16, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceDe__Invoi__534D60F1");

            entity.HasOne(d => d.Procedure).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProcedureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceDe__Proce__5441852A");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC07F3D4C930");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentH__3214EC07C025D2F0");

            entity.ToTable("PaymentHistory");

            entity.Property(e => e.Balance).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.TotalPaid).HasColumnType("decimal(16, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.PaymentHistories)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentHi__Invoi__571DF1D5");
        });

        modelBuilder.Entity<ProcedureList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Procedur__3214EC079774E4E4");

            entity.ToTable("ProcedureList");

            entity.Property(e => e.DefaultUnit)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(16, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.ActivationCode)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LoginId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OauthKey)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("OAuthKey");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGrou__3214EC071224D4DE");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC07F1DC2089");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Module)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("UserProfile");

            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRout__3214EC078FA558B0");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsersInGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersInG__3214EC0736B42B27");

            entity.Property(e => e.AssignBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AssignDate).HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.UsersInGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersInGr__Group__36B12243");

            entity.HasOne(d => d.User).WithMany(p => p.UsersInGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersInGr__UserI__35BCFE0A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
