using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.IdentityModel.Tokens;

namespace webapi.Services;

public class DecodeJwt {
  private readonly GoogleOptions _googleOptions;

  public DecodeJwt(IConfiguration? configuration) {
    // Configure Google authentication options
    _googleOptions = new GoogleOptions();

    if (configuration != null)
      configuration.GetSection("Authentication:Google").Bind(_googleOptions);
  }

  // decode the google jwt token and verify it
  public virtual async Task<JwtSecurityToken?>
  DecodeGoogleJwtToken(string jwtToken) {
    try {
      // Retrieve Google's public keys for token validation
      var httpClient = new HttpClient();
      var jwksJson = await httpClient.GetStringAsync(
          "https://www.googleapis.com/oauth2/v3/certs");
      var jwks = new JsonWebKeySet(jwksJson);

      // Configure token validation parameters
      var tokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,   ValidIssuer = "https://accounts.google.com",
        ValidateAudience = true, ValidAudience = _googleOptions.ClientId,
        ValidateLifetime = true, IssuerSigningKeys = jwks.Keys,
      };

      return DecodeTheJwt(jwtToken, tokenValidationParameters);
    } catch (Exception e) {
      Console.WriteLine(e);
      throw;
    }
  }

  private JwtSecurityToken? DecodeTheJwt(
      string jwtToken, TokenValidationParameters tokenValidationParameters) {
    var tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken validatedToken;
    tokenHandler.ValidateToken(jwtToken, tokenValidationParameters,
                               out validatedToken);

    // Convert SecurityToken to JwtSecurityToken if it is a JWT
    JwtSecurityToken? jwtSecurityToken = validatedToken as JwtSecurityToken;

    return jwtSecurityToken;
  }
}
