using System.ComponentModel.DataAnnotations;
using webapi.Helpers.Enums;
using webapi.Helpers.Validators;

namespace webapi.ApiModels.Register;

public class EmployeeRegistrationModel : RegisterModelBase {
  [Required]
  [EnumDataType(typeof(EmployeeTypes))]
  [EnumValidation<EmployeeTypes>]
  public EmployeeTypes EmployeeType { get; set; }
  [Required]
  [DataType(DataType.Text)]
  public string FirstName {
    get; set;
  } = null!;
  [Required]
  [DataType(DataType.Text)]
  public string LastName {
    get; set;
  } = null!;
}
