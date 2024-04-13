using System.Runtime.InteropServices.JavaScript;
using webapi.Helpers.Enums;

namespace webapi.ApiModels.Login;

public class AuthResponseModel {
  public string AuthTokenType { get; set; } = "Bearer"; // standard bearer
  public string AuthTokenString { get; set; }

  public DateTime AuthTokenExpiration { get; set; }
  public string RefreshToken { get; set; }
  public DateTime RefreshTokenExpiration { get; set; }
  public UserTypes UserType { get; set; }

  public string? ErrorMessage { get; set; }
}
