﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data.Databases;

#nullable disable

namespace webapi.Migrations.disabilityexpertguardians {
[DbContext(typeof(DisabilityExpertGuardiansContext))]
[Migration("20240120090433_InitialDEGC")]
partial class InitialDEGC {
  /// <inheritdoc />
  protected override void BuildTargetModel(ModelBuilder modelBuilder) {
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

    modelBuilder.Entity("webapi.Models.Disabilities.Disability", b => {
      b.Property<int>("DisabilityId")
          .ValueGeneratedOnAdd()
          .HasColumnType("int");

      b.Property<string>("DisabilityDescription")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("DisabilityName")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("DisabilityNameNormalized")
          .IsRequired()
          .HasColumnType("longtext");

      b.Property<string>("Language").IsRequired().HasColumnType("longtext");

      b.HasKey("DisabilityId");

      b.ToTable("Disabilities", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Disabilities.ExpertDisability", b => {
      b.Property<string>("DisabilityExpertId").HasColumnType("varchar(255)");

      b.Property<int>("DisabilityId").HasColumnType("int");

      b.HasKey("DisabilityExpertId", "DisabilityId");

      b.HasIndex("DisabilityId");

      b.ToTable("ExpertDisabilities", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Accounts.DisabilityExpert", b => {
      b.HasBaseType("webapi.Models.Accounts.UserBase");

      b.Property<int>("BirthYear").HasColumnType("int");

      b.Property<string>("FirstName").IsRequired().HasColumnType("longtext");

      b.Property<string>("GuardianId").HasColumnType("varchar(255)");

      b.Property<string>("LastName").IsRequired().HasColumnType("longtext");

      b.Property<string>("PostalCode").HasColumnType("longtext");

      b.Property<string>("TypeBenadering").HasColumnType("longtext");

      b.HasIndex("GuardianId");

      b.ToTable("DisabilityExperts", (string)null);
    });

    modelBuilder.Entity("webapi.Models.Accounts.Guardian", b => {
      b.HasBaseType("webapi.Models.Accounts.UserBase");

      b.Property<int>("BirthYear").HasColumnType("int");

      b.Property<string>("FirstName").IsRequired().HasColumnType("longtext");

      b.Property<string>("LastName").IsRequired().HasColumnType("longtext");

      b.Property<string>("PostalCode").HasColumnType("longtext");

      b.ToTable("Guardians", (string)null);
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

    modelBuilder.Entity("webapi.Models.Disabilities.ExpertDisability", b => {
      b.HasOne("webapi.Models.Accounts.DisabilityExpert", "DisabilityExpert")
          .WithMany("ExpertDisabilities")
          .HasForeignKey("DisabilityExpertId")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

      b.HasOne("webapi.Models.Disabilities.Disability", "Disability")
          .WithMany("ExpertDisabilities")
          .HasForeignKey("DisabilityId")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

      b.Navigation("Disability");

      b.Navigation("DisabilityExpert");
    });

    modelBuilder.Entity("webapi.Models.Accounts.DisabilityExpert", b => {
      b.HasOne("webapi.Models.Accounts.Guardian", "Guardian")
          .WithMany()
          .HasForeignKey("GuardianId");

      b.HasOne("webapi.Models.Accounts.UserBase", null)
          .WithOne()
          .HasForeignKey("webapi.Models.Accounts.DisabilityExpert", "Id")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

      b.Navigation("Guardian");
    });

    modelBuilder.Entity("webapi.Models.Accounts.Guardian", b => {
      b.HasOne("webapi.Models.Accounts.UserBase", null)
          .WithOne()
          .HasForeignKey("webapi.Models.Accounts.Guardian", "Id")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();
    });

    modelBuilder.Entity("webapi.Models.Disabilities.Disability",
                        b => { b.Navigation("ExpertDisabilities"); });

    modelBuilder.Entity("webapi.Models.Accounts.DisabilityExpert",
                        b => { b.Navigation("ExpertDisabilities"); });
#pragma warning restore 612, 618
  }
}
}
