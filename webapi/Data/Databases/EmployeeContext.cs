using Microsoft.EntityFrameworkCore;
using webapi.Data.Bases;
using webapi.Models.Accounts;

namespace webapi.Data.Databases;

public class EmployeeContext
(DbContextOptions<EmployeeContext> options) : IdentityBaseContext(options) {
  public DbSet<Employee> Employees { get; init; } = null!;

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Configure the table name for Employees
    builder.Entity<Employee>().ToTable("Employees");
  }
}
