using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using webapi.Helpers.Validators;

namespace webapi.Helpers.Enums;

// add this attribute to the enum to make it serialize as a string
[JsonConverter(typeof(EnumConverter<OauthProviders>))]
public enum OauthProviders {
  [EnumMember(Value = "Google")] Google,

}
