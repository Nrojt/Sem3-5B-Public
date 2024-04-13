using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using webapi.ApiModels.Login;
using webapi.Helpers;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Controllers.authentication;

[ApiController]
[Route("[controller]")]
public class GoogleLoginController : ControllerBase {
  private readonly DecodeJwt _decodeJwt;
  private readonly UserResolveServiceGoogle _userResolveServiceGoogle;
  private readonly UserResolveService _userResolveService;
  private readonly LoginLogic _loginLogic;

  public GoogleLoginController(
      DecodeJwt decodeJwt, UserResolveServiceGoogle userResolveServiceGoogle,
      LoginLogic loginLogic, UserResolveService userResolveService) {
    _decodeJwt = decodeJwt;
    _userResolveServiceGoogle = userResolveServiceGoogle;
    _loginLogic = loginLogic;
    _userResolveService = userResolveService;
  }

  [HttpPost]
  public async Task<IActionResult>
  GoogleLogin([FromBody] JwtTokenModel jwtTokenModel,
              [FromQuery] bool useCookies = false) {
    // decoding the google jwt token
    JwtSecurityToken? jsonWebToken =
        await _decodeJwt.DecodeGoogleJwtToken(jwtTokenModel.JwtToken);

    if (jsonWebToken == null) {
      return BadRequest("Invalid jwt token");
    }

    // getting the google id from the jwt token
    string? googleId =
        jsonWebToken.Claims.FirstOrDefault(claim => claim.Type == "sub").Value;

    // getting the email from the jwt token
    string? email =
        jsonWebToken.Claims.FirstOrDefault(claim => claim.Type == "email")
            .Value;

    // checking if the google id is null or empty
    if (string.IsNullOrEmpty(googleId)) {
      return BadRequest("Google id is null or empty");
    }

    // checking the database for a user with the google id
    UserBase? user =
        await _userResolveServiceGoogle.GetUserByGoogleIdAsync(googleId);

    if (user == null) {
      // if the user is not found by google id, check if they can be found by
      // email
      bool emailInUseAsync = await _userResolveService.IsEmailInUseAsync(email);

      if (emailInUseAsync) {
        // if the email is in use, return bad request
        return BadRequest("Email already in use");
      }

      return Unauthorized();
    }

    AuthResponseModel authResponseModel =
        await _loginLogic.LoginAuthResponseModel(user);

    if (!string.IsNullOrEmpty(authResponseModel.ErrorMessage)) {
      return BadRequest(authResponseModel.ErrorMessage);
    }

    if (useCookies) {
      return _loginLogic.LoginCookies(authResponseModel, Response);
    }

    return Ok(authResponseModel);
  }
}
