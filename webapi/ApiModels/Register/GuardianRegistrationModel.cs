using System.ComponentModel.DataAnnotations;

namespace webapi.ApiModels.Register;

public class GuardianRegistrationModel : RegisterModelBase {
  [DataType(DataType.Text)]
  public string? FirstName { get; set; }
  [DataType(DataType.Text)]
  public string? LastName {
    get; set;
  }
  [Required]
  [DataType(DataType.Date)]
  public int BirthYear {
    get; set;
  }
}
