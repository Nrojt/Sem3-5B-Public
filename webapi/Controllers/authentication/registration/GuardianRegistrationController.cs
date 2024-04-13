using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.ApiModels.Login;
using webapi.ApiModels.Register;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Services;

namespace webapi.Controllers.authentication.registration;

[ApiController]
[Route("register")]
public class GuardianRegistrationController : ControllerBase {
  private readonly UserManager<Guardian> _guardianManager;
  private readonly RegistrationService _registrationService;

  public GuardianRegistrationController(
      UserManager<Guardian> guardianManager,
      RegistrationService registrationService) {
    _guardianManager = guardianManager;
    _registrationService = registrationService;
  }

  [HttpPost("guardian")]
  public async Task<IActionResult> RegisterGuardian(
      [FromBody] GuardianRegistrationModel guardianRegistrationModel) {

    // checking if the email is already in use
    ErrorResponseModel errorResponseModel =
        await _registrationService.checkRegistrationModel(
            guardianRegistrationModel);

    if (errorResponseModel.ErrorMessage != null) {
      return BadRequest(errorResponseModel.ErrorMessage);
    }

    // creating the user
    Guardian user = new() { Email = guardianRegistrationModel.Email,
                            UserName = guardianRegistrationModel.Email,
                            FirstName = guardianRegistrationModel.FirstName,
                            LastName = guardianRegistrationModel.LastName,
                            BirthYear = guardianRegistrationModel.BirthYear,
                            PostalCode = guardianRegistrationModel.PostalCode,
                            PhoneNumber = guardianRegistrationModel.PhoneNumber,
                            GoogleId = guardianRegistrationModel.ProviderId };

    // creating the user in the database
    var result = await _guardianManager.CreateAsync(
        user, guardianRegistrationModel.Password);

    // checking if the user was created successfully
    if (result.Succeeded) {
      // TODO: Handle post-registration actions (e.g., sending confirmation
      // email)

      // adding the user type to the claims
      await _guardianManager.AddClaimAsync(
          user,
          new Claim(CustomClaimTypes.UserType, nameof(UserTypes.Guardian)));

      return Ok(new { message = "Registration successful" });
    }

    return BadRequest(result.Errors);
  }
}
