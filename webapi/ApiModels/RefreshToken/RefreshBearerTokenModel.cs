using System.ComponentModel.DataAnnotations;
using webapi.Helpers.Enums;
using webapi.Helpers.Validators;

namespace webapi.ApiModels.RefreshToken;

public class RefreshBearerTokenModel {
  [DataType(DataType.Text)]
  public string? RefreshTokenString { get; set; }

  [Required]
  [EnumDataType(typeof(UserTypes))]
  [EnumValidation<UserTypes>]
  public UserTypes UserType { get; set; }
}
