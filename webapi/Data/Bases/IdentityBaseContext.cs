using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Models.Accounts;
using webapi.Models.Authentication;

namespace webapi.Data.Bases;

public abstract class IdentityBaseContext : IdentityDbContext<UserBase> {
  public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
  protected IdentityBaseContext(DbContextOptions options) : base(options) {
    // just passing the options on
    // TODO logging
  }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Configure the table name for the refresh tokens
    builder.Entity<RefreshToken>().ToTable("RefreshTokens");
  }
}
