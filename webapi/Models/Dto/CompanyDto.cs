using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Dto;

public class CompanyDto {
  [Key]
  public string CompanyId { get; set; } = null!;
  public string CompanyName { get; set; } = null!;
}
