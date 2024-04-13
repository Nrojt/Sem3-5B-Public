using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Dto;

public class DisabilityDto {
  [Key]
  public int DisabilityId { get; set; }
}
