using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using webapi.Helpers.Validators;

namespace webapi.Helpers.Enums;

// add this attribute to the enum to make it serialize as a string
[JsonConverter(typeof(EnumConverter<UserTypes>))]
public enum UserTypes {
  [EnumMember(Value =
                  "DisabilityExpertWithGuardian")] DisabilityExpertWithGuardian,
  [
    EnumMember(Value = "DisabilityExpertWithoutGuardian")
  ] DisabilityExpertWithoutGuardian,
  [EnumMember(Value = "Guardian")] Guardian,
  [EnumMember(Value = "Employee")] Employee,
  [EnumMember(Value = "CompanyApproved")] CompanyApproved,
  [EnumMember(Value = "CompanyUnApproved")] CompanyUnApproved
}
