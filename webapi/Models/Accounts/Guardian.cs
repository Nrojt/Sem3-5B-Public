using webapi.Helpers.Enums;

namespace webapi.Models.Accounts;

public class Guardian : HumanBase {

  private UserTypes _userTypes = UserTypes.Guardian; // default value
  public override UserTypes UserType {
    get => _userTypes;
    set { _userTypes = value; }
  }
}
