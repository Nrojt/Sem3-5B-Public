using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using webapi.Helpers.Validators;

namespace webapi.Helpers.Enums;

// add this attribute to the enum to make it serialize as a string
[JsonConverter(typeof(EnumConverter<EmployeeTypes>))]
public enum EmployeeTypes {
  [EnumMember(Value = "Employee")] Employee,
  [EnumMember(Value = "Admin")] Admin,
  [EnumMember(Value = "Moderator")] Moderator,
  [EnumMember(Value = "Support")] Support
}
