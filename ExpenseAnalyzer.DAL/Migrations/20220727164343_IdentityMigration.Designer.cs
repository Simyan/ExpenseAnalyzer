﻿// <auto-generated />
using System;
using ExpenseAnalyzer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    [DbContext(typeof(ExpenseAnalyzerContext))]
    [Migration("20220727164343_IdentityMigration")]
    partial class IdentityMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.BLL.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.BankMaster", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Uid");

                    b.ToTable("BankMaster", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.CategoryMaster", b =>
                {
                    b.Property<short>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Uid"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Uid");

                    b.ToTable("CategoryMaster", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.ReportMetadataMaster", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"), 1L, 1);

                    b.Property<int>("BankMasterUid")
                        .HasColumnType("int")
                        .HasColumnName("BankMasterUId");

                    b.Property<int>("RowIndex")
                        .HasColumnType("int");

                    b.Property<double>("TableArea")
                        .HasColumnType("float");

                    b.Property<string>("TableHeaders")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("TableIndex")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.HasIndex("BankMasterUid")
                        .IsUnique();

                    b.ToTable("ReportMetadataMaster", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.Transaction", b =>
                {
                    b.Property<long>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Uid"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("TypeUid")
                        .HasColumnType("tinyint")
                        .HasColumnName("TypeUId");

                    b.Property<long?>("UserUid")
                        .HasColumnType("bigint");

                    b.Property<long?>("VendorUid")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("VendorUId");

                    b.HasKey("Uid");

                    b.HasIndex("TypeUid");

                    b.HasIndex("UserUid");

                    b.HasIndex("VendorUid");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.TypeMaster", b =>
                {
                    b.Property<byte>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Uid"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Uid");

                    b.ToTable("TypeMaster", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.User", b =>
                {
                    b.Property<long>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Uid"), 1L, 1);

                    b.Property<int?>("BankMasterUid")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Uid");

                    b.HasIndex("BankMasterUid");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.Vendor", b =>
                {
                    b.Property<long>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Uid"), 1L, 1);

                    b.Property<short?>("CategoryMasterUid")
                        .HasColumnType("smallint")
                        .HasColumnName("CategoryMasterUId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<long?>("UserUid")
                        .HasColumnType("bigint");

                    b.HasKey("Uid");

                    b.HasIndex("CategoryMasterUid");

                    b.HasIndex("UserUid");

                    b.ToTable("Vendor", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.ReportMetadataMaster", b =>
                {
                    b.HasOne("ExpenseAnalyzer.DAL.Entities.BankMaster", "BankMaster")
                        .WithOne("ReportMetadataMaster")
                        .HasForeignKey("ExpenseAnalyzer.DAL.Entities.ReportMetadataMaster", "BankMasterUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankMaster");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.Transaction", b =>
                {
                    b.HasOne("ExpenseAnalyzer.DAL.Entities.TypeMaster", "TypeMaster")
                        .WithMany("Transactions")
                        .HasForeignKey("TypeUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseAnalyzer.DAL.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserUid");

                    b.HasOne("ExpenseAnalyzer.DAL.Entities.Vendor", "Vendor")
                        .WithMany("Transactions")
                        .HasForeignKey("VendorUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeMaster");

                    b.Navigation("User");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.User", b =>
                {
                    b.HasOne("ExpenseAnalyzer.DAL.Entities.BankMaster", "Bank")
                        .WithMany("Users")
                        .HasForeignKey("BankMasterUid");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.Vendor", b =>
                {
                    b.HasOne("ExpenseAnalyzer.DAL.Entities.CategoryMaster", "CategoryMaster")
                        .WithMany("Vendors")
                        .HasForeignKey("CategoryMasterUid");

                    b.HasOne("ExpenseAnalyzer.DAL.Entities.User", "User")
                        .WithMany("Vendors")
                        .HasForeignKey("UserUid");

                    b.Navigation("CategoryMaster");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ExpenseAnalyzer.BLL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ExpenseAnalyzer.BLL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseAnalyzer.BLL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ExpenseAnalyzer.BLL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.BankMaster", b =>
                {
                    b.Navigation("ReportMetadataMaster")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.CategoryMaster", b =>
                {
                    b.Navigation("Vendors");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.TypeMaster", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.User", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("Vendors");
                });

            modelBuilder.Entity("ExpenseAnalyzer.DAL.Entities.Vendor", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
