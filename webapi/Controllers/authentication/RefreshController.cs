using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.ApiModels.Login;
using webapi.ApiModels.RefreshToken;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Models.Authentication;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Controllers.authentication;

[ApiController]
[Route("[controller]")]
public class RefreshController : ControllerBase {
  private readonly UserResolveService _userResolveService;
  private readonly AuthTokenService _authTokenService;

  public RefreshController(UserResolveService userResolveService,
                           AuthTokenService authTokenService) {
    _userResolveService = userResolveService;
    _authTokenService = authTokenService;
  }

  // This method is used to refresh the bearer token, so it cannot be authorized
  // (because the bearer token is not valid anymore)
  [HttpPost("refreshbearertoken")]
  public async Task<IActionResult>
  RefreshBearerToken([FromServices] IHttpContextAccessor httpContextAccessor,
                     RefreshBearerTokenModel refreshBearerTokenModel,
                     [FromQuery] bool useCookies = false) {

    // trying to get the refresh token from the model
    string refreshTokenString = refreshBearerTokenModel.RefreshTokenString;

    // If the refresh token is not provided in the model, try to get it from the
    // cookie
    if (string.IsNullOrEmpty(refreshTokenString)) {
      refreshTokenString =
          httpContextAccessor.HttpContext.Request.Cookies["Refresh"];

      if (string.IsNullOrEmpty(refreshTokenString)) {
        // if the refresh token is not provided in the model or in the cookie,
        // return bad request
        return BadRequest("Refresh token not provided");
      }
    }

    // getting the refreshtoken from database based on the refreshtokenstring
    // this is needed to check if the refreshtoken is valid
    RefreshToken? refreshToken =
        await _authTokenService.GetRefreshTokenFromString(
            refreshTokenString, refreshBearerTokenModel.UserType);

    if (refreshToken == null) {
      return BadRequest("Refreshtoken not found for usertype " +
                        refreshBearerTokenModel.UserType);
    }

    if (refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow) {
      return BadRequest("Refreshtoken is invalid");
    }

    // getting the user based on the refreshtoken
    // we need the user information to generate a new bearer token
    UserBase? user = await _userResolveService.GetUserByRefreshTokenAsync(
        refreshToken.Token, refreshBearerTokenModel.UserType);

    // checking if the user exists
    if (user == null) {
      return BadRequest("Invalid refresh token " +
                        refreshBearerTokenModel.RefreshTokenString);
    }

    // getting the associated claims from the user
    var claims = await _userResolveService.GetUserClaimsAsync(user);

    var emailClaim = new Claim(ClaimTypes.Email, user.Email ?? string.Empty);
    var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, user.Id);

    claims.Add(emailClaim);
    claims.Add(nameIdentifierClaim);

    // setting the usertype to the userclaim
    string userTypeClaim =
        claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserType)
            .Value;
    if (userTypeClaim != null) {
      user.UserType = (UserTypes)Enum.Parse(typeof(UserTypes), userTypeClaim);
    } else {
      return BadRequest("User has no usertype claim");
    }

    // generating the new bearer token and refresh token
    var bearerToken = _authTokenService.GenerateJwtToken(claims);

    if (useCookies) {
      // adding the jwt token to the cookies
      var cookieOptionsBearer = _authTokenService.CreateCookieOptions();
      var cookieOptionsRefresh =
          _authTokenService.CreateCookieOptions(refreshToken.Expires);

      Response.Cookies.Append("Bearer", bearerToken, cookieOptionsBearer);
      Response.Cookies.Append("Refresh", refreshToken.Token,
                              cookieOptionsRefresh);
    }

    // Return the token and the user name
    return Ok(new AuthResponseModel() {
      AuthTokenString = bearerToken,
      AuthTokenExpiration =
          DateTime.UtcNow.Add(_authTokenService.GetJwtDuration()),
      RefreshToken = refreshToken.Token,
      RefreshTokenExpiration = refreshToken.Expires, UserType = user.UserType
    });
  }

  [Authorize]
  [HttpGet("newrefreshtoken")]
  public async Task<IActionResult>
  NewRefreshToken([FromQuery] bool useCookies = false) {

    // Extract the email from the claims
    var emailClaim =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

    if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value)) {
      return BadRequest("No email claim found");
    }

    // extracting the user type from the claims
    // getting the usertype claim
    var userTypeClaim =
        HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaimTypes.UserType)
            ?.Value;

    UserTypes userTypeFromClaim;

    if (!string.IsNullOrEmpty(userTypeClaim) &&
        Enum.TryParse(userTypeClaim, out UserTypes parsedType)) {
      userTypeFromClaim = parsedType;
    } else {
      // if the user type is null, return bad request
      return BadRequest("No valid user type found");
    }

    // getting the user from the database
    UserBase? user = await _userResolveService.GetUserByEmailAsync(
        userTypeFromClaim, emailClaim.Value);

    // checking if the user exists
    if (user == null) {
      return BadRequest("User does not exist");
    }

    // getting the refresh token from the user
    RefreshToken? oldRefreshToken =
        await _authTokenService.GetRefreshTokenFromUser(user.Email,
                                                        user.UserType);

    // checking if the user exists
    if (oldRefreshToken == null) {
      return BadRequest(
          "No refresh token found for this user, please log in again");
    }

    // check if the refresh token is still valid
    if (oldRefreshToken.IsRevoked ||
        oldRefreshToken.Expires < DateTime.UtcNow) {
      return BadRequest(
          "Refresh token is not valid anymore, please log in again");
    }

    // generating a new refresh token
    RefreshToken? newRefreshToken =
        await _authTokenService.GenerateNewRefreshToken(userTypeFromClaim);

    // returning if the refresh token is null
    if (newRefreshToken == null) {
      return BadRequest("Could not generate a new refresh token");
    }

    // updating the user in the database, saving the new refresh token
    user.RefreshToken = newRefreshToken;
    await _userResolveService.UpdateAsync(user);

    // removing the old refresh token from the database
    await _authTokenService.RemoveRefreshToken(oldRefreshToken, user.UserType);

    if (useCookies) {
      var cookieOptionsRefresh =
          _authTokenService.CreateCookieOptions(newRefreshToken.Expires);
      Response.Cookies.Append("Refresh", newRefreshToken.Token,
                              cookieOptionsRefresh);
      return Ok("New refresh token set in cookie");
    }

    return Ok(new { refresh = newRefreshToken.Token });
  }
}
