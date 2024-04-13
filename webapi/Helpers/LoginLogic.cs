using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webapi.ApiModels.Login;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Models.Authentication;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Helpers;

public class LoginLogic {
  private readonly UserResolveService _userResolveService;
  private readonly AuthTokenService _authTokenService;

  public LoginLogic(UserResolveService userResolveService,
                    AuthTokenService authTokenService) {
    _userResolveService = userResolveService;
    _authTokenService = authTokenService;
  }

  public async Task<AuthResponseModel> LoginAuthResponseModel(UserBase user) {
    // getting the userclaims from the user in aspnetuserclaims
    var claims = await _userResolveService.GetUserClaimsAsync(user);

    // Generating additional claims
    var emailClaim = new Claim(ClaimTypes.Email, user.Email ?? string.Empty);
    var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, user.Id);

    claims.Add(emailClaim);
    claims.Add(nameIdentifierClaim);

    // setting the usertype to the userclaim
    var userTypeClaim =
        claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserType)
            ?.Value;
    if (userTypeClaim == null) {
      return new AuthResponseModel() { ErrorMessage =
                                           "User has no usertype claim" };
    }

    Console.WriteLine("User has usertype claim");
    user.UserType = (UserTypes)Enum.Parse(typeof(UserTypes), userTypeClaim);

    // if the user is an employee, add the employee type to the claims
    if (user.UserType == UserTypes.Employee) {
      // cast the user to an employee
      Employee employee = (Employee)user;
      claims.Add(new Claim(CustomClaimTypes.EmployeeType,
                           employee.EmployeeType.ToString()));
    }

    // Generating the bearer token (inside a jwt token)
    var tokenString = _authTokenService.GenerateJwtToken(claims);

    // getting the old associated refreshtoken (if any exists)
    RefreshToken? oldRefreshToken =
        await _authTokenService.GetRefreshTokenFromUser(user.Email,
                                                        user.UserType);

    // Generating a new refresh token
    RefreshToken? newRefreshToken =
        await _authTokenService.GenerateNewRefreshToken(user.UserType);

    // returning if the refresh token is null
    if (newRefreshToken == null) {
      return new AuthResponseModel() {
        ErrorMessage = "Could not generate a new refresh token"
      };
    }

    // setting the new refresh token
    user.RefreshToken = newRefreshToken;

    Console.WriteLine("Generated a new refresh token, now updating the user");

    // updating the user in the database, saving the new refresh token
    await _userResolveService.UpdateAsync(user);

    Console.WriteLine(
        "Created new refresh token and updated the user, now deleting the old refresh token");

    // removing the old refresh token from the database (if there is one)
    if (oldRefreshToken != null) {
      await _authTokenService.RemoveRefreshToken(oldRefreshToken,
                                                 user.UserType);
    }

    // expiry date for the bearer
    DateTime authExpiry =
        DateTime.UtcNow.Add(_authTokenService.GetJwtDuration());

    // Return the token and the user name
    return new AuthResponseModel() {
      AuthTokenString = tokenString, AuthTokenExpiration = authExpiry,
      RefreshToken = newRefreshToken.Token,
      RefreshTokenExpiration = newRefreshToken.Expires, UserType = user.UserType
    };
  }

  public IActionResult LoginCookies(AuthResponseModel authResponseModel,
                                    HttpResponse response) {
    // adding the jwt token to the cookies
    var cookieOptionsBearer = _authTokenService.CreateCookieOptions();
    var cookieOptionsRefresh = _authTokenService.CreateCookieOptions(
        authResponseModel.RefreshTokenExpiration);

    response.Cookies.Append("Bearer", authResponseModel.AuthTokenString,
                            cookieOptionsBearer);
    response.Cookies.Append("Refresh", authResponseModel.RefreshToken,
                            cookieOptionsRefresh);

    return new OkObjectResult(authResponseModel);
  }
}
