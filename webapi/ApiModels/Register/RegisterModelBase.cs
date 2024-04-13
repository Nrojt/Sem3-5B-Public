using System.ComponentModel.DataAnnotations;
using webapi.Helpers.Enums;

namespace webapi.ApiModels.Register;

public abstract class RegisterModelBase {
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [DataType(DataType.Password)]
  [Required]
  [Compare("Password",
           ErrorMessage =
               "The password and confirmation password do not match.")]
  public string ConfirmPassword { get; set; }

  [DataType(DataType.PostalCode)]
  public string? PostalCode { get; set; }
  [DataType(DataType.PhoneNumber)]
  public string? PhoneNumber { get; set; }

  // for when logging in with google (and maybe other future oauth providers)
  public string? JwtToken { get; set; }
  public OauthProviders? Provider { get; set; }
  public string? ProviderId { get; set; }
}
