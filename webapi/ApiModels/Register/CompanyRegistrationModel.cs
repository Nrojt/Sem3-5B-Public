using System.ComponentModel.DataAnnotations;

namespace webapi.ApiModels.Register;

public class CompanyRegistrationModel : RegisterModelBase {
  [Required]
  [DataType(DataType.Text)]
  public string CompanyName { get; set; } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string CompanyDescription {
    get; set;
  } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string CompanyAddress {
    get; set;
  } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string CompanyCity {
    get; set;
  } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string CompanyCountry {
    get; set;
  } = null!;

  [Required]
  [DataType(DataType.Url)]
  public string CompanyWebsite {
    get; set;
  } = null!;
}
