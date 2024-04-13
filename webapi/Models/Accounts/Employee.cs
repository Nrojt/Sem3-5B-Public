using webapi.Helpers.Enums;

namespace webapi.Models.Accounts;

public class Employee : HumanBase {

  private UserTypes _userTypes = UserTypes.Employee; // default value
  public override UserTypes UserType {
    get => _userTypes;
    set { _userTypes = value; }
  }
  public virtual EmployeeTypes EmployeeType {
    get; set;
  } = EmployeeTypes.Employee;
}
