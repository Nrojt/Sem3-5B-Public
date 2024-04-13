using Microsoft.EntityFrameworkCore;
using webapi.Data.Bases;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;

namespace webapi.Data.Databases;

// Database context for the disability experts and guardians
public class DisabilityExpertGuardiansContext
(DbContextOptions<DisabilityExpertGuardiansContext> options)
    : IdentityBaseContext(options) {
  public DbSet<DisabilityExpert> DisabilityExperts { get; init; } = null!;
  public DbSet<Disability> Disabilities { get; init; } = null!;
  public DbSet<Guardian> Guardians { get; init; } = null!;

  // New DbSet for the many-to-many relationship
  public DbSet<ExpertDisability> ExpertDisabilities { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Configure the table name for DisabilityExpert
    builder.Entity<DisabilityExpert>().ToTable("DisabilityExperts");
    // Configure the table name for DisabilityExpert
    builder.Entity<Disability>().ToTable("Disabilities");
    // Configure the table name for Guardians
    builder.Entity<Guardian>().ToTable("Guardians");
    // Configure the table name for the many-to-many relationship
    // ExpertDisability
    builder.Entity<ExpertDisability>().ToTable("ExpertDisabilities");

    // Configure the many-to-many relationship
    builder.Entity<ExpertDisability>().HasKey(
        ed => new { ed.DisabilityExpertId, ed.DisabilityId });

    builder.Entity<ExpertDisability>()
        .HasOne(ed => ed.DisabilityExpert)
        .WithMany(de => de.ExpertDisabilities)
        .HasForeignKey(ed => ed.DisabilityExpertId);

    builder.Entity<ExpertDisability>()
        .HasOne(ed => ed.Disability)
        .WithMany(d => d.ExpertDisabilities)
        .HasForeignKey(ed => ed.DisabilityId);
  }
}
