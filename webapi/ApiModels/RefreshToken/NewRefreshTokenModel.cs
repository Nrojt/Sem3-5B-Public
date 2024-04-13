using System.ComponentModel.DataAnnotations;
using webapi.Helpers.Enums;
using webapi.Helpers.Validators;

namespace webapi.ApiModels.RefreshToken;

public class NewRefreshTokenModel {
  [Required]
  public string OldRefreshToken { get; set; } = null!;
  [Required]
  [EnumDataType(typeof(UserTypes))]
  [EnumValidation<UserTypes>]
  public UserTypes UserType { get; set; }
}
