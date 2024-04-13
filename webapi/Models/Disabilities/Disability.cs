using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models.Disabilities;

// Disability
public class Disability {
  public Disability() { ExpertDisabilities = new List<ExpertDisability>(); }

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int DisabilityId { get; set; }
  [Required]
  public string DisabilityName { get; set; } = null!;
  [Required]
  public string DisabilityNameNormalized { get; set; } = null!;
  [Required]
  public string DisabilityDescription { get; set; } = null!;

  [Required(ErrorMessage = "Language is required")]
  [RegularExpression(
      @"^[A-Z]{2}$",
      ErrorMessage = "Language must be 2 uppercase letters, the country code")]
  [DataType(DataType.Text)]
  public string Language { get; set; } = null!;

  // Navigation property for the many-to-many relationship
  public List<ExpertDisability> ExpertDisabilities { get; set; }
}
