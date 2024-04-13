using System.ComponentModel.DataAnnotations;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;
using webapi.Models.Dto;

namespace webapi.Models;

public class Research {
  [Key]
  public int ResearchId { get; set; }
  [Required]
  public string Title { get; set; } = null!;
  [Required]
  public List<DisabilityDto>? Disabilities { get; set; } = null!;
  [Required]
  public List<DisabilityExpertDto> DisabilityExperts { get; set; }
  [Required]
  public Company Company { get; set; } = null!;
  public string AgeRange { get; set; }
  [Required]
  public string Description { get; set; } = null!;
  [Required]
  public DateTime Date { get; set; }
  [Required]
  public string Location { get; set; } = null!;
  public string Reward { get; set; }
  [Required]
  public string ResearchType { get; set; } = null!;
}
