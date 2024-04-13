using System.Runtime.Serialization;
using Newtonsoft.Json;
using webapi.Helpers.Validators;

namespace webapi_unit_tests.Helpers.Enums;

// Test enum
[JsonConverter(typeof(EnumConverter<Color>))]
public enum Color {
  [EnumMember(Value = "Red")] Red,
  [EnumMember(Value = "Green")] Green,
  [EnumMember(Value = "Blue")] Blue
}
