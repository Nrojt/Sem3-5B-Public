﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data.Databases;

#nullable disable

namespace webapi.Migrations.companyresearch {
[DbContext(typeof(CompanyResearchContext))]
partial class CompanyResearchContextModelSnapshot : ModelSnapshot {
  protected override void BuildModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
    modelBuilder.HasAnnotation("ProductVersion", "8.0.1")
        .HasAnnotation("Relational:MaxIdentifierLength", 64);

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b => {
      b.Property<string>("Id").HasColumnType("varchar(255)");

      b.Property<string>("ConcurrencyStamp")
          .IsConcurrencyToken()
          .HasColumnType("longtext");

      b.Property<string>("Name").HasMaxLength(256).HasColumnType(
          "varchar(256)");

      b.Property<string>("NormalizedName")
          .HasMaxLength(256)
          .HasColumnType("varchar(256)");

      b.HasKey("Id");

      b.HasIndex("NormalizedName").IsUnique().HasDatabaseName("RoleNameIndex");

      b.ToTable("AspNetRoles", (string)null);
    });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b => {
          b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");

          b.Property<string>("ClaimType").HasColumnType("longtext");

          b.Property<string>("ClaimValue").HasColumnType("longtext");

          b.Property<string>("RoleId").IsRequired().HasColumnType(
              "varchar(255)");

          b.HasKey("Id");

          b.HasIndex("RoleId");

          b.ToTable("AspNetRoleClaims", (string)null);
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b => {
          b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");

          b.Property<string>("ClaimType").HasColumnType("longtext");

          b.Property<string>("ClaimValue").HasColumnType("longtext");

          b.Property<string>("UserId").IsRequired().HasColumnType(
              "varchar(255)");

          b.HasKey("Id");

          b.HasIndex("UserId");

          b.ToTable("AspNetUserClaims", (string)null);
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b => {
          b.Property<string>("LoginProvider").HasColumnType("varchar(255)");

          b.Property<string>("ProviderKey").HasColumnType("varchar(255)");

          b.Property<string>("ProviderDisplayName").HasColumnType("longtext");

          b.Property<string>("UserId").IsRequired().HasColumnType(
              "varchar(255)");

          b.HasKey("LoginProvider", "ProviderKey");

          b.HasIndex("UserId");

          b.ToTable("AspNetUserLogins", (string)null);
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b => {
          b.Property<string>("UserId").HasColumnType("varchar(255)");

          b.Property<string>("RoleId").HasColumnType("varchar(255)");

          b.HasKey("UserId", "RoleId");

          b.HasIndex("RoleId");

          b.ToTable("AspNetUserRoles", (string)null);
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b => {
          b.Property<string>("UserId").HasColumnType("varchar(255)");

          b.Property<string>("LoginProvider").HasColumnType("varchar(255)");

          b.Property<string>("Name").HasColumnType("varchar(255)");

          b.Property<string>("Value").HasColumnType("longtext");

          b.HasKey("UserId", "LoginProvider", "Name");

          b.ToTable("AspNetUserTokens", (string)null);
        });

    modelBuilder.Entity("webapi.Models.Accounts.UserBase", b => {
      b.Property<string>("Id").HasColumnType("varchar(255)");

      b.Property<int>("AccessFailedCount").HasColumnType("int");

      b.Property<string>("ConcurrencyStamp")
          .IsConcurrencyToken()
          .HasColumnType("longtext");

      b.Property<string>("Email").HasMaxLength(256).HasColumnType(
          "varchar(256)");

      b.Property<bool>("EmailConfirmed").HasColumnType("tinyint(1)");

      b.Property<string>("GoogleId").HasColumnType("longtext");

      b.Property<bool>("LockoutEnabled").HasColumnType("tinyint(1)");

      b.Property<DateTimeOffset?>("LockoutEnd").HasColumnType("datetime(6)");

      b.Property<string>("NormalizedEmail")
          .HasMaxLength(256)
          .HasColumnType("varchar(256)");

      b.Property<string>("NormalizedUserName")
          .HasMaxLength(256)
          .HasColumnType("varchar(256)");

      b.Property<string>("PasswordHash").HasColumnType("longtext");

      b.Property<string>("PhoneNumber").HasColumnType("longtext");

      b.Property<bool>("PhoneNumberConfirmed").HasColumnType("tinyint(1)");

      b.Property<int?>("RefreshTokenId").HasColumnType("int");

      b.Property<string>("SecurityStamp").HasColumnType("longtext");

      b.Property<bool>("TwoFactorEnabled").HasColumnType("tinyint(1)");

      b.Property<string>("UserName")
          .HasMaxLength(256)
          .HasColumnType("varchar(256)");

      b.Property<int>("UserType").HasColumnType("int");

      b.HasKey("Id");

      b.HasIndex("NormalizedEmail").HasDatabaseName("EmailIndex");

      b.HasIndex("NormalizedUserName")
          .IsUnique()
          .HasDatabaseName("UserNameIndex");

      b.HasIndex("RefreshTokenId");

      b.ToTable("AspNetUsers", (string)null);

      b.UseTptMappingStrategy();
    });

    modelBuilder.Entity("webapi.Models.Authentication.RefreshToken", b => {
      b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");

      b.Property<DateTime>("Expires").HasColumnType("datetime(6)");

      b.Property<bool>("IsRevoked").HasColumnType("tinyint(1)");

      b.Property<DateTime>("LastModified").HasColumnType("datetime(6)");

      b.Property<string>("Token").IsRequired().HasColumnType("longtext");

      b.HasKey("Id");

      b.ToTable("RefreshTokens", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Dto.DisabilityDto", b => {
      b.Property<int>("DisabilityId")
          .ValueGeneratedOnAdd()
          .HasColumnType("int");

      b.Property<int?>("ResearchId").HasColumnType("int");

      b.HasKey("DisabilityId");

      b.HasIndex("ResearchId");

      b.ToTable("DisabilityDto");
    });

    modelBuilder.Entity("webapi.Models.Dto.DisabilityExpertDto", b => {
      b.Property<string>("DisabilityExpertId").HasColumnType("varchar(255)");

      b.Property<int?>("ResearchId").HasColumnType("int");

      b.HasKey("DisabilityExpertId");

      b.HasIndex("ResearchId");

      b.ToTable("DisabilityExpertDto");
    });

    modelBuilder.Entity("webapi.Models.Research", b => {
      b.Property<int>("ResearchId").ValueGeneratedOnAdd().HasColumnType("int");

      b.Property<string>("AgeRange").IsRequired().HasColumnType("longtext");

      b.Property<string>("CompanyId")
          .IsRequired()
          .HasColumnType("varchar(255)");

      b.Property<DateTime>("Date").HasColumnType("datetime(6)");

      b.Property<string>("Description").IsRequired().HasColumnType("longtext");

      b.Property<string>("Location").IsRequired().HasColumnType("longtext");

      b.Property<string>("ResearchType").IsRequired().HasColumnType("longtext");

      b.Property<string>("Reward").IsRequired().HasColumnType("longtext");

      b.Property<string>("Title").IsRequired().HasColumnType("longtext");

      b.HasKey("ResearchId");

      b.HasIndex("CompanyId");

      b.ToTable("Researches", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Tracking.Tracking", b => {
      b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");

      b.Property<string>("Action").IsRequired().HasColumnType("longtext");

      b.Property<string>("Category").IsRequired().HasColumnType("longtext");

      b.Property<string>("CompanyId")
          .IsRequired()
          .HasColumnType("varchar(255)");

      b.Property<string>("EventName").IsRequired().HasColumnType("longtext");

      b.Property<string>("Label").IsRequired().HasColumnType("longtext");

      b.Property<DateTime?>("TimeStamp")
          .ValueGeneratedOnAdd()
          .HasColumnType("datetime(6)")
          .HasDefaultValueSql("NOW()");

      b.Property<string>("UserId").IsRequired().HasColumnType("longtext");

      b.HasKey("Id");

      b.HasIndex("CompanyId");

      b.ToTable("Tracking", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Accounts.Company", b => {
      b.HasBaseType("webapi.Models.Accounts.UserBase");

      b.Property<string>("ApiKey").HasColumnType("longtext");

      b.Property<bool>("Approved").HasColumnType("tinyint(1)");

      b.Property<string>("CompanyAddress")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("CompanyCity").IsRequired().HasColumnType("longtext");

      b.Property<string>("CompanyCountry")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("CompanyDescription")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("CompanyName").IsRequired().HasColumnType("longtext");

      b.Property<string>("CompanyPostalCode")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("CompanyWebsite")
          .IsRequired()
          .HasColumnType("longtext");

      b.ToTable("Companies", (string)null);
    });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b => {
          b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
              .WithMany()
              .HasForeignKey("RoleId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b => {
          b.HasOne("webapi.Models.Accounts.UserBase", null)
              .WithMany()
              .HasForeignKey("UserId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b => {
          b.HasOne("webapi.Models.Accounts.UserBase", null)
              .WithMany()
              .HasForeignKey("UserId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b => {
          b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
              .WithMany()
              .HasForeignKey("RoleId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

          b.HasOne("webapi.Models.Accounts.UserBase", null)
              .WithMany()
              .HasForeignKey("UserId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
        });

    modelBuilder.Entity(
        "Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b => {
          b.HasOne("webapi.Models.Accounts.UserBase", null)
              .WithMany()
              .HasForeignKey("UserId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
        });

    modelBuilder.Entity("webapi.Models.Accounts.UserBase", b => {
      b.HasOne("webapi.Models.Authentication.RefreshToken", "RefreshToken")
          .WithMany()
          .HasForeignKey("RefreshTokenId");

      b.Navigation("RefreshToken");
    });

    modelBuilder.Entity("webapi.Models.Dto.DisabilityDto", b => {
      b.HasOne("webapi.Models.Research", null)
          .WithMany("Disabilities")
          .HasForeignKey("ResearchId");
    });

    modelBuilder.Entity("webapi.Models.Dto.DisabilityExpertDto", b => {
      b.HasOne("webapi.Models.Research", null)
          .WithMany("DisabilityExperts")
          .HasForeignKey("ResearchId");
    });

    modelBuilder.Entity("webapi.Models.Research", b => {
      b.HasOne("webapi.Models.Accounts.Company", "Company")
          .WithMany()
          .HasForeignKey("CompanyId")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

      b.Navigation("Company");
    });

    modelBuilder.Entity("webapi.Models.Tracking.Tracking", b => {
      b.HasOne("webapi.Models.Accounts.Company", "Company")
          .WithMany()
          .HasForeignKey("CompanyId")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

      b.Navigation("Company");
    });

    modelBuilder.Entity("webapi.Models.Accounts.Company", b => {
      b.HasOne("webapi.Models.Accounts.UserBase", null)
          .WithOne()
          .HasForeignKey("webapi.Models.Accounts.Company", "Id")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();
    });

    modelBuilder.Entity("webapi.Models.Research", b => {
      b.Navigation("Disabilities");

      b.Navigation("DisabilityExperts");
    });
#pragma warning restore 612, 618
  }
}
}
