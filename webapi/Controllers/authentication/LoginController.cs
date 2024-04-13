using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapi.ApiModels.Login;
using webapi.Helpers;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Controllers.authentication;
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase {

  private readonly UserResolveService _userResolveService;
  private readonly LoginLogic _loginLogic;

  public LoginController(UserResolveService userResolveService,
                         LoginLogic loginLogic) {
    _userResolveService = userResolveService;
    _loginLogic = loginLogic;
  }

  [HttpPost]
  public async Task<IActionResult> Login(LoginModel loginModel,
                                         [FromQuery] bool useCookies = false) {

    // checking if the user exists and if the password is correct
    UserBase? user =
        await _userResolveService.GetUserByEmailAsync(loginModel.Email);

    if (user == null) {
      return BadRequest("Cannot login with provided credentials");
    }

    if (await _userResolveService.CheckPasswordAsync(user,
                                                     loginModel.Password)) {

      AuthResponseModel authResponseModel =
          await _loginLogic.LoginAuthResponseModel(user);

      if (!authResponseModel.ErrorMessage.IsNullOrEmpty()) {
        return BadRequest(authResponseModel.ErrorMessage);
      }

      if (useCookies) {
        return _loginLogic.LoginCookies(authResponseModel, Response);
      }

      return Ok(authResponseModel);
    }

    return Unauthorized("Cannot login with provided credentials");
  }
}
