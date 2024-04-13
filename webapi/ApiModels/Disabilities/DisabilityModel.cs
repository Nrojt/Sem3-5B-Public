using System.ComponentModel.DataAnnotations;

namespace webapi.ApiModels.Disabilities;

public class DisabilityModel {
  public int? DisabilityId { get; set; }

  [Required]
  [DataType(DataType.Text)]
  public string DisabilityName { get; set; } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string DisabilityDescription { get; set; } = null!;

  [Required(ErrorMessage = "Language is required")]
  [RegularExpression(
      @"^[A-Z]{2}$",
      ErrorMessage = "Language must be 2 uppercase letters, the country code")]
  [DataType(DataType.Text)]
  public string Language { get; set; } = "NL";
}
