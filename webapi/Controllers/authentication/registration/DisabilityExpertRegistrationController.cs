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
public class DisabilityExpertRegistrationController : ControllerBase {
  private readonly UserManager<DisabilityExpert> _disabilityExpertManager;
  private readonly RegistrationService _registrationService;

  public DisabilityExpertRegistrationController(
      UserManager<DisabilityExpert> disabilityExpertManager,
      RegistrationService registrationService) {
    _disabilityExpertManager = disabilityExpertManager;
    _registrationService = registrationService;
  }

  [HttpPost("disabilityexpert")]
  public async Task<IActionResult> RegisterDisabilityExpert([
    FromBody
  ] DisabilityExpertRegistrationModel disabilityExpertRegistrationModel) {

    // checking if the email is already in use
    ErrorResponseModel errorResponseModel =
        await _registrationService.checkRegistrationModel(
            disabilityExpertRegistrationModel);

    if (errorResponseModel.ErrorMessage != null) {
      return BadRequest(errorResponseModel.ErrorMessage);
    }

    // creating a new disabilityexpert and setting the properties
    DisabilityExpert user =
        new() { Email = disabilityExpertRegistrationModel.Email,
                UserName = disabilityExpertRegistrationModel.Email,
                FirstName = disabilityExpertRegistrationModel.FirstName,
                LastName = disabilityExpertRegistrationModel.LastName,
                BirthYear = disabilityExpertRegistrationModel.BirthYear,
                PostalCode = disabilityExpertRegistrationModel.PostalCode,
                PhoneNumber = disabilityExpertRegistrationModel.PhoneNumber,
                GoogleId = disabilityExpertRegistrationModel.ProviderId };

    // creating the user in the database
    var result = await _disabilityExpertManager.CreateAsync(
        user, disabilityExpertRegistrationModel.Password);

    // checking if the user was created successfully
    if (result.Succeeded) {
      // TODO: Handle post-registration actions (e.g., sending confirmation
      // email)

      // adding the user type to the claims
      await _disabilityExpertManager.AddClaimAsync(
          user, new Claim(CustomClaimTypes.UserType,
                          nameof(UserTypes.DisabilityExpertWithoutGuardian)));

      return Ok(new { message = "Registration successful" });
    }

    // if the user was not created successfully, return the errors (f.e. email
    // already exists or password is not strong enough)
    return BadRequest(result.Errors);
  }
}
