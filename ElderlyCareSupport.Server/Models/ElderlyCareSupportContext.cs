using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCareSupport.Server.Models;

public partial class ElderlyCareSupportContext : DbContext
{
    public ElderlyCareSupportContext()
    {
    }

    public ElderlyCareSupportContext(DbContextOptions<ElderlyCareSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FeeConfiguration> FeeConfigurations { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
