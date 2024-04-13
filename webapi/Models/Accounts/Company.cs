using webapi.Helpers.Enums;

namespace webapi.Models.Accounts;

public class Company : UserBase {
  // company account
  public string CompanyName { get; set; } = null!;
  public string CompanyDescription { get; set; } = null!;
  public string CompanyAddress { get; set; } = null!;
  public string CompanyCity { get; set; } = null!;
  public string CompanyCountry { get; set; } = null!;
  public string CompanyPostalCode { get; set; } = null!;
  public string CompanyWebsite { get; set; } = null!;
  public string? ApiKey { get; set; }

  // company accounts need to be approved by an admin
  public bool Approved { get; set; }

  private UserTypes _userTypes = UserTypes.CompanyUnApproved; // default value

  public override UserTypes UserType {
    get => _userTypes;
    set { _userTypes = value; }
  }
}
