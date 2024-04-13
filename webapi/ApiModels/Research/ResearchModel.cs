using System.ComponentModel.DataAnnotations;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;
using webapi.Models.Dto;

namespace webapi.ApiModels.Research;

public class ResearchModel {
  [Required]
  public string Title { get; set; } = null!;
  public List<DisabilityDto>? Disabilities { get; set; } = null!;
  public List<DisabilityExpertDto>? DisabilityExperts { get; set; } = null!;
  public string AgeRange { get; set; } = null!;
  [Required]
  public string Description { get; set; } = null!;
  [Required]
  public DateTime Date { get; set; }
  [Required]
  public string Location { get; set; } = null!;
  public string? Reward { get; set; }
  [Required]
  public string ResearchType { get; set; } = null!;
  public string? CompanyName { get; set; }
  public int? ResearchId { get; set; }
}
