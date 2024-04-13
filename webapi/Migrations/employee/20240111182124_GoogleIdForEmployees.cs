﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations.employee {
/// <inheritdoc />
public partial class GoogleIdForEmployees : Migration {
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder) {
    migrationBuilder
        .AddColumn<string>(name: "GoogleId", table: "AspNetUsers",
                           type: "longtext", nullable: true)
        .Annotation("MySql:CharSet", "utf8mb4");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder) {
    migrationBuilder.DropColumn(name: "GoogleId", table: "AspNetUsers");
  }
}
}
