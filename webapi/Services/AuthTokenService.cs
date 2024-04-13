using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Data.Bases;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;
using webapi.Models.Authentication;

namespace webapi.Services;

public class AuthTokenService {
  private readonly IIdbContextFactory _iidbContextFactory;

  // caching items from the configuration for performance
  private readonly SymmetricSecurityKey _jwtKey;
  private readonly TimeSpan _jwtDuration;
  private readonly TimeSpan _refreshTokenDuration;
  private readonly string _issuer;
  private readonly string _audience;

  public AuthTokenService(IConfiguration configuration,
                          IIdbContextFactory iidbContextFactory) {
    // context factory for getting the correct db context
    _iidbContextFactory = iidbContextFactory;

    // getting the jwt key from the configuration
    var jwtKey = configuration["Jwt:Key"];
    if (jwtKey == null) {
      throw new InvalidOperationException("JWT Key is not configured");
    }

    _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

    // getting the jwt duration from the configuration and converting it to a
    // timespan
    int jwtDurationInMinutes;
    if (!int.TryParse(configuration["Jwt:DurationInMinutes"],
                      out jwtDurationInMinutes)) {
      throw new InvalidOperationException(
          "JWT Duration is not configured or invalid");
    }
    _jwtDuration = TimeSpan.FromMinutes(jwtDurationInMinutes);

    // getting the refresh token duration from the configuration and converting
    // it to a timespan
    int refreshTokenDurationInDays;
    if (!int.TryParse(configuration["Jwt:RefreshTokenDurationInDays"],
                      out refreshTokenDurationInDays)) {
      throw new InvalidOperationException(
          "Refresh Token Duration is not configured or invalid");
    }
    _refreshTokenDuration = TimeSpan.FromDays(refreshTokenDurationInDays);

    // getting the issuer and audience from the configuration
    _issuer = configuration["Jwt:Issuer"] ??
              throw new InvalidOperationException("Issuer is not configured");
    _audience =
        configuration["Jwt:Audience"] ??
        throw new InvalidOperationException("Audience is not configured");
  }

  // method for generating a jwt token (bearer)
  public string GenerateJwtToken(List<Claim> claims) {
    // generate token that is valid for the amount of time specified
    var creds = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.UtcNow.Add(_jwtDuration);

    // create the token
    var token =
        new JwtSecurityToken(_issuer, _audience, claims, expires: expires,
                             signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  // creating cookie options
  public CookieOptions CreateCookieOptions(DateTime expires) {
    // Setting the JWT token in an HttpOnly cookie
    CookieOptions cookieOptions = new CookieOptions {
      HttpOnly = true, Secure = true,
      SameSite = SameSiteMode.None, // Needed cause of the CORS policy
      Expires = expires
    };
    Console.WriteLine("Cookies expire at " + cookieOptions.Expires +
                      ". It is now " + DateTime.UtcNow);
    return cookieOptions;
  }

  // creating cookie options for bearer token, need expiration date for the
  // cookie from configuration
  public CookieOptions CreateCookieOptions() {
    // get the expiration date from the configuration
    DateTime expires =
        DateTime.UtcNow.Add(_jwtDuration); // cookie will expire after
    // the token expires, cause
    // cookieoptions are created
    // after the token is created

    return CreateCookieOptions(expires);
  }

  // method for generating a refresh token
  private RefreshToken GenerateRefreshToken() {
    RefreshToken refreshToken = new RefreshToken();
    refreshToken.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    refreshToken.Expires = DateTime.UtcNow.Add(_refreshTokenDuration);
    refreshToken.LastModified = DateTime.UtcNow;
    return refreshToken;
  }

  // method for generating a new refresh token, with null check
  public async Task<RefreshToken?>
  GenerateNewRefreshToken(UserTypes? userType) {
    IdentityBaseContext? context = _iidbContextFactory.GetContext(userType);

    // if the context is null, we return null
    if (context == null) {
      return null;
    }

    // generate a new refresh token, try 5 times to make sure it is unique
    bool isUnique = false;
    const int MAX_RETRIES = 5;
    RefreshToken? newRefreshToken = null;

    // use a for loop instead of a do-while loop
    for (int i = 0; i < MAX_RETRIES; i++) {
      // use a try-catch block to handle exceptions
      try {
        newRefreshToken = GenerateRefreshToken();
        isUnique = await IsRefreshTokenUnique(newRefreshToken);
        if (isUnique)
          break;
      } catch (Exception ex) {
        // log the error for further investigation
        // TODO: use your preferred logging framework
        Console.WriteLine(ex.Message);
        // set the new refresh token to null
        newRefreshToken = null;
      }
    }

    // This should never happen, but if it does, we should log it. GUID can
    // theoretically be duplicated, but the chance is so small that it is
    // negligible.
    if (!isUnique) {
      // log the error for further investigation
      // TODO: Log
      Console.WriteLine(
          "Failed to generate a unique refresh token after {0} attempts",
          MAX_RETRIES);
      // set the new refresh token to null
      newRefreshToken = null;
    }

    return newRefreshToken;
  }

  // method for removing a refresh token from the database
  public async Task RemoveRefreshToken(RefreshToken refreshToken,
                                       UserTypes userType) {
    var context = _iidbContextFactory.GetContext(userType);
    context.RefreshTokens.Remove(refreshToken);
    await context.SaveChangesAsync();
  }

  // getting the refresh token from the user (by email, which is unique)
  public async Task<RefreshToken?> GetRefreshTokenFromUser(string email,
                                                           UserTypes userType) {
    IdentityBaseContext? context = _iidbContextFactory.GetContext(userType);

    if (context == null) {
      return null;
    }

    UserBase? userWithToken =
        await context.Users
            .Include(u => u.RefreshToken) // Include the RefreshToken navigation
            // property
            .FirstOrDefaultAsync(u => u.Email == email);

    RefreshToken? refreshToken = userWithToken?.RefreshToken;

    return refreshToken;
  }

  // method for getting RefreshToken from the database based on the string
  public async Task<RefreshToken?>
  GetRefreshTokenFromString(string refreshTokenString, UserTypes userTypes) {
    var context = _iidbContextFactory.GetContext(userTypes);
    RefreshToken? refreshToken =
        await context.RefreshTokens.FirstOrDefaultAsync(
            rt => rt.Token == refreshTokenString);

    return refreshToken;
  }

  // method for checking refresh tokens across all databases
  private async Task<bool> IsRefreshTokenUnique(RefreshToken refreshToken) {
    // creating a list for storing the already used contexts
    var usedContexts = new List<IdentityBaseContext>();

    // looping over all the user types and getting the db contexts for each
    foreach (UserTypes userType in UserTypes.GetValues(typeof(UserTypes))) {
      // getting the context for the user type
      IdentityBaseContext? context = _iidbContextFactory.GetContext(userType);

      // if the context is null, we skip it
      if (context == null)
        continue;

      // if the context is already used, we skip it
      if (usedContexts.Contains(context)) {
        Console.WriteLine("Already used context");
        continue;
      }

      // add the context to the used contexts list
      usedContexts.Add(context);

      // checking if the token is already used in the database
      bool isDuplicate = await context.RefreshTokens.AnyAsync(
          rt => rt.Token == refreshToken.Token);

      // exit the loop if the token is already used
      if (isDuplicate) {
        return false;
      }
    }

    // return true if any of the results is true, otherwise false
    return true;
  }

  public TimeSpan GetJwtDuration() { return _jwtDuration; }
}
