using System.IdentityModel.Tokens.Jwt;
using webapi.ApiModels.Login;
using webapi.ApiModels.Register;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Services.userresolvers;

namespace webapi.Services;

// Class for common registration tasks
public class RegistrationService {

  private readonly UserResolveService _userResolveService;
  private readonly UserResolveServiceGoogle _userResolveServiceGoogle;
  private readonly DecodeJwt _decodeJwt;

  public RegistrationService(UserResolveService userResolveService,
                             UserResolveServiceGoogle userResolveServiceGoogle,
                             DecodeJwt decodeJwt) {
    _userResolveService = userResolveService;
    _userResolveServiceGoogle = userResolveServiceGoogle;
    _decodeJwt = decodeJwt;
  }

  public virtual async Task<ErrorResponseModel>
  checkRegistrationModel(RegisterModelBase registerModel) {
    // setting up the error response model
    ErrorResponseModel errorResponseModel =
        new ErrorResponseModel() { RegisterModelBase = registerModel,
                                   ErrorMessage = null };

    // checking if the email is already in use
    string? emailErrorMessage = await checkEmail(registerModel.Email);

    if (!string.IsNullOrEmpty(emailErrorMessage)) {
      errorResponseModel.ErrorMessage = emailErrorMessage;
      return errorResponseModel;
    }

    // checking google
    if (registerModel.Provider != null &&
        registerModel.Provider == OauthProviders.Google) {
      // checking the google id and setting the information from the jwt token
      await checkGoogle(registerModel, errorResponseModel);

      if (!string.IsNullOrEmpty(errorResponseModel.ErrorMessage)) {
        return errorResponseModel;
      }
    }

    // checking if the email is the standard placeholder email
    if (registerModel.Email == "placeholder@email.com") {
      errorResponseModel.ErrorMessage = "Email is not allowed";
      return errorResponseModel;
    }

    return errorResponseModel;
  }

  private async Task<string?> checkEmail(string email) {
    // checking if the email is already in use
    bool emailInUse = await _userResolveService.IsEmailInUseAsync(email);
    if (emailInUse) {
      return "Email is already in use";
    }

    return null;
  }

  private async Task checkGoogle(RegisterModelBase registerModel,
                                 ErrorResponseModel errorResponseModel) {
    // decoding the google jwt token
    JwtSecurityToken? jsonWebToken =
        await _decodeJwt.DecodeGoogleJwtToken(registerModel.JwtToken);

    if (jsonWebToken == null) {
      errorResponseModel.ErrorMessage = "Invalid jwt token";
      return;
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
      errorResponseModel.ErrorMessage = "Google id is null or empty";
      return;
    }

    // checking the database for a user with the google id
    bool isUnique = await _userResolveServiceGoogle.IsGoogleIdUnique(googleId);

    if (isUnique == false) {
      errorResponseModel.ErrorMessage = "Google id is already in use";
      return;
    }

    // setting the information from the google jwt token to the register model
    registerModel.Email = email;
    registerModel.ProviderId = googleId;

    // if the user is a disabilityexpert or a guardian, also get the first and
    // last name
    if (registerModel is DisabilityExpertRegistrationModel ||
        registerModel is GuardianRegistrationModel) {
      string? firstName =
          jsonWebToken.Claims
              .FirstOrDefault(claim => claim.Type == "given_name")
              .Value;
      string? lastName =
          jsonWebToken.Claims
              .FirstOrDefault(claim => claim.Type == "family_name")
              .Value;

      if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) {
        errorResponseModel.ErrorMessage =
            "First name or last name is null or empty";
        return;
      }

      // cast the register model to the correct type
      if (registerModel is DisabilityExpertRegistrationModel
              disabilityExpertRegistrationModel) {
        disabilityExpertRegistrationModel.FirstName = firstName;
        disabilityExpertRegistrationModel.LastName = lastName;
      } else if (registerModel is GuardianRegistrationModel
                     guardianRegistrationModel) {
        guardianRegistrationModel.FirstName = firstName;
        guardianRegistrationModel.LastName = lastName;
      }
    }
  }
}
