using webapi.Data.Bases;
using webapi.Data.Databases;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;

namespace webapi.Services.database;

public class IdbContextFactory : IIdbContextFactory {
  private readonly IServiceProvider _serviceProvider;

  public IdbContextFactory(IServiceProvider serviceProvider) {
    _serviceProvider = serviceProvider;
  }

  // This method returns the correct db context for the user type
  // This method needs to be changed if a new user type is added
  // The return type is IdentityBaseContext, which is the base class for all the
  // db contexts. This means the properies of the child classes are not
  // available
  public IdentityBaseContext? GetContext(UserTypes? userType) {
    switch (userType) {
    case UserTypes.CompanyApproved:
    case UserTypes.CompanyUnApproved:
      Console.WriteLine("Returning CompanyResearchContext");
      return _serviceProvider.GetRequiredService<CompanyResearchContext>();
    case UserTypes.DisabilityExpertWithGuardian:
    case UserTypes.DisabilityExpertWithoutGuardian:
    case UserTypes.Guardian:
      Console.WriteLine("Returning DisabilityExpertGuardiansContext");
      return _serviceProvider
          .GetRequiredService<DisabilityExpertGuardiansContext>();
    case UserTypes.Employee:
      Console.WriteLine("Returning EmployeeContext");
      return _serviceProvider.GetRequiredService<EmployeeContext>();
    default:
      Console.Error.WriteLine("User type " + userType + " is not supported");
      return null;
    }
  }
}
