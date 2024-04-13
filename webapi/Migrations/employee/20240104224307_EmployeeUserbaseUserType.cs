using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations.employee {
/// <inheritdoc />
public partial class EmployeeUserbaseUserType : Migration {
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder) {
    migrationBuilder.AddColumn<int>(name: "UserType", table: "AspNetUsers",
                                    type: "int", nullable: false,
                                    defaultValue: 0);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder) {
    migrationBuilder.DropColumn(name: "UserType", table: "AspNetUsers");
  }
}
}
