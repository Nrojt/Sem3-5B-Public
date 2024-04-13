using Microsoft.EntityFrameworkCore;
using webapi.Data.Bases;
using webapi.Models;
using webapi.Models.Accounts;
using webapi.Models.Tracking;

namespace webapi.Data.Databases;

public class CompanyResearchContext
(DbContextOptions<CompanyResearchContext> options)
    : IdentityBaseContext(options) {
  public DbSet<Company> Companies { get; init; } = null!;

  public DbSet<Tracking> Tracking { get; init; } = null!;
  public DbSet<Research> Researches { get; init; } = null!;

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Configure the table name for Companies
    builder.Entity<Company>().ToTable("Companies");
    // Configure the table name for Trackings
    builder.Entity<Tracking>().ToTable("Tracking");
    // configure the table name for Researches
    builder.Entity<Research>().ToTable("Researches");

    // adding default value for timestamp in tracking
    builder.Entity<Tracking>()
        .Property(t => t.TimeStamp)
        .HasDefaultValueSql("NOW()");
  }
}
