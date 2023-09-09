using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WS_Caja6.Models;

public partial class DbCajaContext : DbContext
{
    public DbCajaContext()
    {
    }

    public DbCajaContext(DbContextOptions<DbCajaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    public virtual DbSet<SystemRole> SystemRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D6AINSB\\ADMIN;Database=DbCaja;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3213E83FA7A88AC9");

            entity.ToTable("Company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("businessName");
            entity.Property(e => e.Cbu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cbu");
            entity.Property(e => e.CompanyType).HasColumnName("companyType");
            entity.Property(e => e.TaxId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("taxId");
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemAc__3213E83FF0E335B4");

            entity.ToTable("SystemAccount");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .HasColumnName("passwordHash");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .HasColumnName("refreshToken");
            entity.Property(e => e.UserName)
                .HasMaxLength(400)
                .HasColumnName("userName");

            entity.HasMany(d => d.SystemRoles).WithMany(p => p.SystemAccounts)
                .UsingEntity<Dictionary<string, object>>(
                    "SystemAccountSystemRole",
                    r => r.HasOne<SystemRole>().WithMany()
                        .HasForeignKey("SystemRoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SystemAccountSystemRole_SystemRole"),
                    l => l.HasOne<SystemAccount>().WithMany()
                        .HasForeignKey("SystemAccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SystemAccountSystemRole_SystemAccount"),
                    j =>
                    {
                        j.HasKey("SystemAccountId", "SystemRoleId");
                        j.ToTable("SystemAccountSystemRole");
                    });
        });

        modelBuilder.Entity<SystemRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemRo__3213E83F40C553F1");

            entity.ToTable("SystemRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsBackOfficeRol).HasColumnName("isBackOfficeRol");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
