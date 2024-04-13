using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
[Authorize(Policy = "Admin")]
public class EmployeeRegistrationController : ControllerBase {
  private readonly UserManager<Employee> _employeeManager;
  private readonly RegistrationService _registrationService;

  public EmployeeRegistrationController(
      UserManager<Employee> employeeManager,
      RegistrationService registrationService) {
    _employeeManager = employeeManager;
    _registrationService = registrationService;
  }

  [HttpPost("employee")]
  public async Task<IActionResult> RegisterEmployee(
      [FromBody] EmployeeRegistrationModel employeeRegistrationModel) {

    // checking if the email is already in use
    ErrorResponseModel errorResponseModel =
        await _registrationService.checkRegistrationModel(
            employeeRegistrationModel);

    if (errorResponseModel.ErrorMessage != null) {
      return BadRequest(errorResponseModel.ErrorMessage);
    }

    // creating the user
    Employee user =
        new() { Email = employeeRegistrationModel.Email,
                UserName = employeeRegistrationModel.Email,
                EmployeeType = employeeRegistrationModel.EmployeeType,
                FirstName = employeeRegistrationModel.FirstName,
                LastName = employeeRegistrationModel.LastName };

    // creating the user in the database
    var result = await _employeeManager.CreateAsync(
        user, employeeRegistrationModel.Password);

    // checking if the user was created successfully
    if (result.Succeeded) {
      // adding the user type to the claims
      await _employeeManager.AddClaimAsync(
          user,
          new Claim(CustomClaimTypes.UserType, nameof(UserTypes.Employee)));

      return Ok(new { message = "Registration successful" });
    }

    return BadRequest(result.Errors);
  }
}
