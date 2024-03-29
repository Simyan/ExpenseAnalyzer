﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseAnalyzer.Entities
{
    public partial class ExpenseAnalyzerContext : DbContext
    {
        public ExpenseAnalyzerContext()
        {
        }

        public ExpenseAnalyzerContext(DbContextOptions<ExpenseAnalyzerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryMaster> CategoryMasters { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TypeMaster> TypeMasters { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI;Database=ExpenseAnalyzer;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Vendor");

                entity.Property(e => e.Uid).HasColumnName("UId");
                entity.Property(e => e.CategoryMasterUid).HasColumnName("CategoryMasterUId");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("CategoryMaster");

                entity.Property(e => e.Uid).HasColumnName("UId");


                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>()
               .HasOne<Vendor>(e => e.Vendor)
               .WithMany(e => e.Transactions)
               .HasForeignKey(e => e.VendorUid);

            modelBuilder.Entity<Transaction>()
                .HasOne<TypeMaster>(e => e.TypeMaster)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.TypeUid);

            modelBuilder.Entity<Vendor>()
                .HasOne<CategoryMaster>(e => e.CategoryMaster)
                .WithMany(e => e.Vendors)
                .HasForeignKey(e => e.CategoryMasterUid);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Transaction");

                entity.Property(e => e.Uid).HasColumnName("UId");

                

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PostingDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TypeUid).HasColumnName("TypeUId");
                entity.Property(e => e.VendorUid).HasColumnName("VendorUId");
            });

            modelBuilder.Entity<TypeMaster>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("TypeMaster");

                entity.Property(e => e.Uid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UId");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
