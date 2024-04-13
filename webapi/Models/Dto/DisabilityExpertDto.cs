using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Dto;

public class DisabilityExpertDto {
  [Key]
  public string DisabilityExpertId { get; set; } = null!;
  [Required]
  public string FirstName { get; set; } = null!;
  [Required]
  public string LastName { get; set; } = null!;
}
