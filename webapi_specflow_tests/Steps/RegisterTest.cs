using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using webapi_specflow_tests.Helpers;
using webapi.ApiModels.Login;
using webapi.ApiModels.Register;
using webapi.Controllers.authentication.registration;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi_specflow_tests.Steps;

[Binding]
public class RegisterTest {
  private UserTypes _userType;
  // registerbases
  private DisabilityExpertRegistrationModel? _disabilityExpertRegistrationModel;
  private GuardianRegistrationModel? _guardianRegistrationModel;
  private CompanyRegistrationModel? _companyRegistrationModel;

  [Given(@"the user sends a request to register")]
  public void GivenTheUserSendsARequestToRegister() {
    // Initialize the registration models
    _disabilityExpertRegistrationModel =
        new DisabilityExpertRegistrationModel();
    _guardianRegistrationModel = new GuardianRegistrationModel();
    _companyRegistrationModel = new CompanyRegistrationModel();
  }

  [When(@"the user is of type (.*)")]
  public void WhenTheUserIsOfType(string userTypeString) {
    _userType = (UserTypes)Enum.Parse(typeof(UserTypes), userTypeString);
  }

  [When(@"all the required fields are filled")]
  public void WhenAllTheRequiredFieldsAreFilled() {
    // base fields which are required for all users
    string email = "johndoe@email.com";
    string password = "password#A123";
    string postalCode = "1234AB";

    // depending on the user type, fill the required fields
    switch (_userType) {
    case UserTypes.DisabilityExpertWithoutGuardian:
      if (_disabilityExpertRegistrationModel != null) {
        _disabilityExpertRegistrationModel.Email = email;
        _disabilityExpertRegistrationModel.Password = password;
        _disabilityExpertRegistrationModel.ConfirmPassword = password;
        _disabilityExpertRegistrationModel.PostalCode = postalCode;

        _disabilityExpertRegistrationModel.FirstName = "John";
        _disabilityExpertRegistrationModel.LastName = "Doe";
        _disabilityExpertRegistrationModel.BirthYear = 1999;
      } else {
        Assert.Fail("DisabilityExpertRegistrationModel is null");
      }

      break;

    case UserTypes.Guardian:
      if (_guardianRegistrationModel != null) {
        _guardianRegistrationModel.Email = email;
        _guardianRegistrationModel.Password = password;
        _guardianRegistrationModel.ConfirmPassword = password;
        _guardianRegistrationModel.PostalCode = postalCode;

        _guardianRegistrationModel.FirstName = "John";
        _guardianRegistrationModel.LastName = "Doe";
        _guardianRegistrationModel.BirthYear = 1999;
      } else {
        Assert.Fail("GuardianRegistrationModel is null");
      }

      break;
    case UserTypes.CompanyApproved:
      if (_companyRegistrationModel != null) {
        _companyRegistrationModel.Email = email;
        _companyRegistrationModel.Password = password;
        _companyRegistrationModel.ConfirmPassword = password;
        _companyRegistrationModel.PostalCode = postalCode;

        _companyRegistrationModel.CompanyName = "Company";
        _companyRegistrationModel.CompanyDescription = "Company description";
        _companyRegistrationModel.CompanyAddress = "Company address";
        _companyRegistrationModel.CompanyCity = "Company city";
        _companyRegistrationModel.CompanyCountry = "Company country";
        _companyRegistrationModel.CompanyWebsite = "https://www.company.com";
      } else {
        Assert.Fail("CompanyRegistrationModel is null");
      }
      break;
    default:
      Assert.Fail("User type not found");
      break;
    }
  }

  [Then(@"the user is registered")]
  public async Task ThenTheUserIsRegistered() {
    // mocking the usermanagers
    var mockUserManagerCompany = MockUserManagers.MockUserManager<Company>();
    mockUserManagerCompany.Object.UserValidators.Add(
        new UserValidator<Company>());
    mockUserManagerCompany.Object.PasswordValidators.Add(
        new PasswordValidator<Company>());

    var mockUserManagerDisabilityExpert =
        MockUserManagers.MockUserManager<DisabilityExpert>();
    mockUserManagerDisabilityExpert.Object.UserValidators.Add(
        new UserValidator<DisabilityExpert>());
    mockUserManagerDisabilityExpert.Object.PasswordValidators.Add(
        new PasswordValidator<DisabilityExpert>());

    var mockUserManagerGuardian = MockUserManagers.MockUserManager<Guardian>();
    mockUserManagerGuardian.Object.UserValidators.Add(
        new UserValidator<Guardian>());
    mockUserManagerGuardian.Object.PasswordValidators.Add(
        new PasswordValidator<Guardian>());

    // setting the return value of the createAsync method to an identity result
    // with success
    mockUserManagerCompany
        .Setup(x => x.CreateAsync(It.IsAny<Company>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success);
    mockUserManagerDisabilityExpert
        .Setup(x => x.CreateAsync(It.IsAny<DisabilityExpert>(),
                                  It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success);
    mockUserManagerGuardian
        .Setup(x => x.CreateAsync(It.IsAny<Guardian>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success);

    mockUserManagerCompany
        .Setup(x => x.AddClaimAsync(It.IsAny<Company>(), It.IsAny<Claim>()))
        .ReturnsAsync(IdentityResult.Success);
    mockUserManagerDisabilityExpert
        .Setup(x => x.AddClaimAsync(It.IsAny<DisabilityExpert>(),
                                    It.IsAny<Claim>()))
        .ReturnsAsync(IdentityResult.Success);
    mockUserManagerGuardian
        .Setup(x => x.AddClaimAsync(It.IsAny<Guardian>(), It.IsAny<Claim>()))
        .ReturnsAsync(IdentityResult.Success);

    // mocking the registration service
    var mockDecodeJwt = new Mock<DecodeJwt>(null!);
    mockDecodeJwt.Setup(x => x.DecodeGoogleJwtToken(It.IsAny<string>()))
        .ReturnsAsync(new JwtSecurityToken());

    var mockRegistrationService = new Mock<RegistrationService>(
        new UserResolveService(new Dictionary<UserTypes, IUserManagerWrapper>(),
                               null!),
        new UserResolveServiceGoogle(null!), mockDecodeJwt.Object);

    // setting the return value of the checkRegistrationModel method to an empty
    // error response model
    mockRegistrationService
        .Setup(x => x.checkRegistrationModel(It.IsAny<RegisterModelBase>()))
        .ReturnsAsync(new ErrorResponseModel());

    // depending on the user type, call the register method
    CompanyRegistrationController companyRegistrationController =
        new CompanyRegistrationController(mockUserManagerCompany.Object,
                                          mockRegistrationService.Object);
    DisabilityExpertRegistrationController
        disabilityExpertRegistrationController =
            new DisabilityExpertRegistrationController(
                mockUserManagerDisabilityExpert.Object,
                mockRegistrationService.Object);
    GuardianRegistrationController guardianRegistrationController =
        new GuardianRegistrationController(mockUserManagerGuardian.Object,
                                           mockRegistrationService.Object);

    IActionResult result;
    switch (_userType) {
    case UserTypes.DisabilityExpertWithoutGuardian:
      result =
          await disabilityExpertRegistrationController.RegisterDisabilityExpert(
              _disabilityExpertRegistrationModel);
      Assert.Equal(typeof(OkObjectResult), result.GetType());
      break;
    case UserTypes.Guardian:
      result = await guardianRegistrationController.RegisterGuardian(
          _guardianRegistrationModel);
      Assert.Equal(typeof(OkObjectResult), result.GetType());
      break;
    case UserTypes.CompanyApproved:
      result = await companyRegistrationController.RegisterCompany(
          _companyRegistrationModel);
      Assert.Equal(typeof(OkObjectResult), result.GetType());
      break;
    default:
      Assert.Fail("User type not found");
      break;
    }
  }
}
