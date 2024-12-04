using System;
using System.Collections.Generic;
using ElderlyCareSupport.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Contexts;

public partial class ElderlyCareSupportContext : DbContext
{
    public ElderlyCareSupportContext()
    {
    }

    public ElderlyCareSupportContext(DbContextOptions<ElderlyCareSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ElderCareAccount> ElderCareAccounts { get; set; }

    public virtual DbSet<FeeConfiguration> FeeConfigurations { get; set; }

    public virtual DbSet<VolunteerAccount> VolunteerAccounts { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElderCareAccount>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__ElderCar__3214EC07457AF218");

            entity.ToTable("ElderCareAccount");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(200);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(200);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Region).HasMaxLength(200);
        });

        modelBuilder.Entity<FeeConfiguration>(entity =>
        {
            entity.HasKey(e => e.FeeId).HasName("PK__FEE_CONF__EC233B49F893270E");

            entity.ToTable("FEE_CONFIGURATION");

            entity.Property(e => e.FeeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(17, 0)")
                .HasColumnName("FEE_ID");
            entity.Property(e => e.FeeAmount)
                .HasColumnType("numeric(17, 0)")
                .HasColumnName("FEE_AMOUNT");
            entity.Property(e => e.FeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FEE_NAME");
        });

        modelBuilder.Entity<VolunteerAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VolunteerAccount");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(200);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(200);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Region).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
