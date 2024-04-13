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
public class CompanyRegistrationController : ControllerBase {
  private readonly UserManager<Company> _companyManager;

  private readonly RegistrationService _registrationService;

  public CompanyRegistrationController(
      UserManager<Company> companyManager,
      RegistrationService registrationService) {
    _companyManager = companyManager;
    _registrationService = registrationService;
  }

  [HttpPost("company")]
  public async Task<IActionResult> RegisterCompany(
      [FromBody] CompanyRegistrationModel companyRegistrationModel) {

    // checking if the email is already in use
    ErrorResponseModel errorResponseModel =
        await _registrationService.checkRegistrationModel(
            companyRegistrationModel);

    if (errorResponseModel.ErrorMessage != null) {
      return BadRequest(errorResponseModel.ErrorMessage);
    }

    // creating the user
    Company user =
        new() { Email = companyRegistrationModel.Email,
                UserName = companyRegistrationModel.Email,
                CompanyName = companyRegistrationModel.CompanyName,
                CompanyDescription =
                    companyRegistrationModel.CompanyDescription,
                CompanyAddress = companyRegistrationModel.CompanyAddress,
                CompanyCity = companyRegistrationModel.CompanyCity,
                CompanyCountry = companyRegistrationModel.CompanyCountry,
                CompanyPostalCode = companyRegistrationModel.PostalCode,
                CompanyWebsite = companyRegistrationModel.CompanyWebsite,
                PhoneNumber = companyRegistrationModel.PhoneNumber,
                Approved = false,
                GoogleId = companyRegistrationModel.ProviderId };

    // creating the user in the database
    var result = await _companyManager.CreateAsync(
        user, companyRegistrationModel.Password);

    // checking if the user was created successfully
    if (result.Succeeded) {
      // TODO: Handle post-registration actions (e.g., sending confirmation
      // email)

      // adding the user type to the claims
      await _companyManager.AddClaimAsync(
          user, new Claim(CustomClaimTypes.UserType,
                          nameof(UserTypes.CompanyUnApproved)));

      return Ok(new { message = "Registration successful" });
    }

    return BadRequest(result.Errors);
  }
}
