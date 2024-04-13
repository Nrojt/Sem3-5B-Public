using webapi.Helpers.Enums;
using webapi.Models.Disabilities;

namespace webapi.Models.Accounts;

public class DisabilityExpert : HumanBase {
  public DisabilityExpert() {
    ExpertDisabilities = new List<ExpertDisability>();
  }

  private UserTypes _userTypes =
      UserTypes.DisabilityExpertWithoutGuardian; // default value
  public override UserTypes UserType {
    get => _userTypes;
    set { _userTypes = value; }
  }

  public Guardian? Guardian { get; set; }
  public string? TypeBenadering { get; set; }
  // Navigation property for the many-to-many relationship
  public List<ExpertDisability>? ExpertDisabilities { get; set; }
}
