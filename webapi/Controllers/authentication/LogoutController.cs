using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Controllers.authentication;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase {
  private readonly UserResolveService _userResolveService;
  private readonly AuthTokenService _authTokenService;

  public LogoutController(UserResolveService userResolveService,
                          AuthTokenService authTokenService) {
    _userResolveService = userResolveService;
    _authTokenService = authTokenService;
  }

  [Authorize]
  [HttpPost]
  public async Task<IActionResult> Logout([FromQuery] bool useCookies = false) {
    // getting the email from the claims
    var emailClaim =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
    if (emailClaim == null)
      return BadRequest("No email claim found");

    // getting the usertype claim
    var userTypeClaimValue =
        HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaimTypes.UserType)
            ?.Value;

    UserTypes userTypeClaim;

    if (!string.IsNullOrEmpty(userTypeClaimValue) &&
        Enum.TryParse(userTypeClaimValue, out UserTypes parsedType)) {
      userTypeClaim = parsedType;
    } else {
      return BadRequest("No valid usertype claim found");
    }

    // getting the user based on the email
    UserBase? user = await _userResolveService.GetUserByEmailAsync(
        userTypeClaim, emailClaim.Value);

    if (user == null) {
      return BadRequest("No user found with email " + emailClaim.Value +
                        " and usertype " + userTypeClaim);
    }

    // getting the refreshtoken from the user
    if (user?.RefreshToken == null) {
      user.RefreshToken = await _authTokenService.GetRefreshTokenFromUser(
          user.Email, user.UserType);
    }

    // checking if the user has a refresh token, it should always have one
    if (user?.RefreshToken != null) {
      // invalidating the refresh token
      user.RefreshToken.IsRevoked = true;
      await _userResolveService.UpdateAsync(user);

      // deleting the cookies if present
      if (useCookies) {
        Console.WriteLine("Deleting cookies");
        // Get the response cookies
        var responseCookies = Response.Cookies;

        // Delete the cookie by name
        responseCookies.Delete("Bearer");
        responseCookies.Delete("Refresh");
      }

      return Ok(new { message = "Logout successful" });
    }

    return BadRequest("No refresh token found for this user " +
                      (user.Email ?? string.Empty) + " with usertype " +
                      user.UserType);
  }
}
