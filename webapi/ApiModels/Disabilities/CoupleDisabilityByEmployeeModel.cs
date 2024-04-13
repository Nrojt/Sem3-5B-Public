using System.ComponentModel.DataAnnotations;

namespace webapi.ApiModels.Disabilities;

public class CoupleDisabilityByEmployeeModel {
  [Required]
  public int DisabilityId { get; set; }
  [Required]
  public string DisabilityExpertId { get; set; }
}
