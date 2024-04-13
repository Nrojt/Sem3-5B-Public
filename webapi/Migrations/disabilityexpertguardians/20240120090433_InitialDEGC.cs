using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations.disabilityexpertguardians {
/// <inheritdoc />
public partial class InitialDEGC : Migration {
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder) {
    migrationBuilder.AlterDatabase().Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder
        .CreateTable(
            name: "AspNetRoles",
            columns: table =>
                new { Id = table
                               .Column<string>(type: "varchar(255)",
                                               nullable: false)
                               .Annotation("MySql:CharSet", "utf8mb4"),
                      Name = table
                                 .Column<string>(type: "varchar(256)",
                                                 maxLength: 256, nullable: true)
                                 .Annotation("MySql:CharSet", "utf8mb4"),
                      NormalizedName =
                          table
                              .Column<string>(type: "varchar(256)",
                                              maxLength: 256, nullable: true)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                      ConcurrencyStamp =
                          table.Column<string>(type: "longtext", nullable: true)
                              .Annotation("MySql:CharSet", "utf8mb4") },
            constraints: table =>
            { table.PrimaryKey("PK_AspNetRoles", x => x.Id); })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder
        .CreateTable(
            name: "Disabilities",
            columns: table =>
                new { DisabilityId =
                          table.Column<int>(type: "int", nullable: false)
                              .Annotation(
                                  "MySql:ValueGenerationStrategy",
                                  MySqlValueGenerationStrategy.IdentityColumn),
                      DisabilityName =
                          table
                              .Column<string>(type: "longtext", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                      DisabilityNameNormalized =
                          table
                              .Column<string>(type: "longtext", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                      DisabilityDescription =
                          table
                              .Column<string>(type: "longtext", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                      Language =
                          table
                              .Column<string>(type: "longtext", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4") },
            constraints: table =>
            { table.PrimaryKey("PK_Disabilities", x => x.DisabilityId); })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder
        .CreateTable(
            name: "RefreshTokens",
            columns: table =>
                new { Id = table.Column<int>(type: "int", nullable: false)
                               .Annotation(
                                   "MySql:ValueGenerationStrategy",
                                   MySqlValueGenerationStrategy.IdentityColumn),
                      Token =
                          table
                              .Column<string>(type: "longtext", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                      IsRevoked = table.Column<bool>(type: "tinyint(1)",
                                                     nullable: false),
                      LastModified = table.Column<DateTime>(type: "datetime(6)",
                                                            nullable: false),
                      Expires = table.Column<DateTime>(type: "datetime(6)",
                                                       nullable: false) },
            constraints: table =>
            { table.PrimaryKey("PK_RefreshTokens", x => x.Id); })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                 .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                 RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                          .Annotation("MySql:CharSet", "utf8mb4"),
                          ClaimType = table.Column<string>(type: "longtext", nullable: true)
                                      .Annotation("MySql:CharSet", "utf8mb4"),
                                      ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                                                   .Annotation("MySql:CharSet", "utf8mb4")
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            table.ForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                column: x => x.RoleId,
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
        {
            Id = table.Column<string>(type: "varchar(255)", nullable: false)
                 .Annotation("MySql:CharSet", "utf8mb4"),
                 UserType = table.Column<int>(type: "int", nullable: false),
                 RefreshTokenId = table.Column<int>(type: "int", nullable: true),
                 GoogleId = table.Column<string>(type: "longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                            UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                       NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                               Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                                       NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                                               EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                                               PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                                                       SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                                                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                                                               ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                                                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                                                                       PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                                                                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                                                                               PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                                                                               TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                                                                               LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                                                                                               LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                                                                               AccessFailedCount = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            table.ForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                column: x => x.RefreshTokenId,
                principalTable: "RefreshTokens",
                principalColumn: "Id");
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                 .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                 UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                          .Annotation("MySql:CharSet", "utf8mb4"),
                          ClaimType = table.Column<string>(type: "longtext", nullable: true)
                                      .Annotation("MySql:CharSet", "utf8mb4"),
                                      ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                                                   .Annotation("MySql:CharSet", "utf8mb4")
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            table.ForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                column: x => x.UserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
        {
            LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                            ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                                          .Annotation("MySql:CharSet", "utf8mb4"),
                                          ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                                                  .Annotation("MySql:CharSet", "utf8mb4"),
                                                  UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                                                          .Annotation("MySql:CharSet", "utf8mb4")
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetUserLogins", x => new {
                x.LoginProvider, x.ProviderKey
            });
            table.ForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                column: x => x.UserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
        {
            UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                     .Annotation("MySql:CharSet", "utf8mb4"),
                     RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                              .Annotation("MySql:CharSet", "utf8mb4")
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetUserRoles", x => new {
                x.UserId, x.RoleId
            });
            table.ForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                column: x => x.RoleId,
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                column: x => x.UserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
        {
            UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                     .Annotation("MySql:CharSet", "utf8mb4"),
                     LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                     Name = table.Column<string>(type: "varchar(255)", nullable: false)
                                            .Annotation("MySql:CharSet", "utf8mb4"),
                                            Value = table.Column<string>(type: "longtext", nullable: true)
                                                    .Annotation("MySql:CharSet", "utf8mb4")
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_AspNetUserTokens", x => new {
                x.UserId, x.LoginProvider, x.Name
            });
            table.ForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                column: x => x.UserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "Guardians",
            columns: table => new
        {
            Id = table.Column<string>(type: "varchar(255)", nullable: false)
                 .Annotation("MySql:CharSet", "utf8mb4"),
                 FirstName = table.Column<string>(type: "longtext", nullable: false)
                             .Annotation("MySql:CharSet", "utf8mb4"),
                             LastName = table.Column<string>(type: "longtext", nullable: false)
                                        .Annotation("MySql:CharSet", "utf8mb4"),
                                        PostalCode = table.Column<string>(type: "longtext", nullable: true)
                                                .Annotation("MySql:CharSet", "utf8mb4"),
                                                BirthYear = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Guardians", x => x.Id);
            table.ForeignKey(
                name: "FK_Guardians_AspNetUsers_Id",
                column: x => x.Id,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "DisabilityExperts",
            columns: table => new
        {
            Id = table.Column<string>(type: "varchar(255)", nullable: false)
                 .Annotation("MySql:CharSet", "utf8mb4"),
                 GuardianId = table.Column<string>(type: "varchar(255)", nullable: true)
                              .Annotation("MySql:CharSet", "utf8mb4"),
                              TypeBenadering = table.Column<string>(type: "longtext", nullable: true)
                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                               FirstName = table.Column<string>(type: "longtext", nullable: false)
                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                                       LastName = table.Column<string>(type: "longtext", nullable: false)
                                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                                               PostalCode = table.Column<string>(type: "longtext", nullable: true)
                                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                                                       BirthYear = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_DisabilityExperts", x => x.Id);
            table.ForeignKey(
                name: "FK_DisabilityExperts_AspNetUsers_Id",
                column: x => x.Id,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_DisabilityExperts_Guardians_GuardianId",
                column: x => x.GuardianId,
                principalTable: "Guardians",
                principalColumn: "Id");
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateTable(
            name: "ExpertDisabilities",
            columns: table => new
        {
            DisabilityExpertId = table.Column<string>(type: "varchar(255)", nullable: false)
                                 .Annotation("MySql:CharSet", "utf8mb4"),
                                 DisabilityId = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_ExpertDisabilities", x => new {
                x.DisabilityExpertId, x.DisabilityId
            });
            table.ForeignKey(
                name: "FK_ExpertDisabilities_Disabilities_DisabilityId",
                column: x => x.DisabilityId,
                principalTable: "Disabilities",
                principalColumn: "DisabilityId",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_ExpertDisabilities_DisabilityExperts_DisabilityExpertId",
                column: x => x.DisabilityExpertId,
                principalTable: "DisabilityExperts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        })
        .Annotation("MySql:CharSet", "utf8mb4");

    migrationBuilder.CreateIndex(name: "IX_AspNetRoleClaims_RoleId",
                                 table: "AspNetRoleClaims", column: "RoleId");

    migrationBuilder.CreateIndex(name: "RoleNameIndex", table: "AspNetRoles",
                                 column: "NormalizedName", unique: true);

    migrationBuilder.CreateIndex(name: "IX_AspNetUserClaims_UserId",
                                 table: "AspNetUserClaims", column: "UserId");

    migrationBuilder.CreateIndex(name: "IX_AspNetUserLogins_UserId",
                                 table: "AspNetUserLogins", column: "UserId");

    migrationBuilder.CreateIndex(name: "IX_AspNetUserRoles_RoleId",
                                 table: "AspNetUserRoles", column: "RoleId");

    migrationBuilder.CreateIndex(name: "EmailIndex", table: "AspNetUsers",
                                 column: "NormalizedEmail");

    migrationBuilder.CreateIndex(name: "IX_AspNetUsers_RefreshTokenId",
                                 table: "AspNetUsers",
                                 column: "RefreshTokenId");

    migrationBuilder.CreateIndex(name: "UserNameIndex", table: "AspNetUsers",
                                 column: "NormalizedUserName", unique: true);

    migrationBuilder.CreateIndex(name: "IX_DisabilityExperts_GuardianId",
                                 table: "DisabilityExperts",
                                 column: "GuardianId");

    migrationBuilder.CreateIndex(name: "IX_ExpertDisabilities_DisabilityId",
                                 table: "ExpertDisabilities",
                                 column: "DisabilityId");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder) {
    migrationBuilder.DropTable(name: "AspNetRoleClaims");

    migrationBuilder.DropTable(name: "AspNetUserClaims");

    migrationBuilder.DropTable(name: "AspNetUserLogins");

    migrationBuilder.DropTable(name: "AspNetUserRoles");

    migrationBuilder.DropTable(name: "AspNetUserTokens");

    migrationBuilder.DropTable(name: "ExpertDisabilities");

    migrationBuilder.DropTable(name: "AspNetRoles");

    migrationBuilder.DropTable(name: "Disabilities");

    migrationBuilder.DropTable(name: "DisabilityExperts");

    migrationBuilder.DropTable(name: "Guardians");

    migrationBuilder.DropTable(name: "AspNetUsers");

    migrationBuilder.DropTable(name: "RefreshTokens");
  }
}
}
