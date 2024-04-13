using Microsoft.EntityFrameworkCore;
using webapi.Data.Bases;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;

namespace webapi.Services.userresolvers;

public class UserResolveServiceGoogle {
  private readonly IIdbContextFactory _iidbContextFactory;

  public UserResolveServiceGoogle(IIdbContextFactory iidbContextFactory) {
    _iidbContextFactory = iidbContextFactory;
  }

  public async Task<UserBase?> GetUserByGoogleIdAsync(string googleId) {
    // creating a list for storing the already used contexts
    var usedContexts = new List<IdentityBaseContext>();

    // looping over all the user types and getting the db contexts for each
    foreach (UserTypes userType in Enum.GetValues(typeof(UserTypes))) {
      // getting the context for the user type
      IdentityBaseContext? context = _iidbContextFactory.GetContext(userType);

      // if the context is null, we skip it
      if (context == null)
        continue;

      // if the context is already used, we skip it
      if (usedContexts.Contains(context)) {
        Console.WriteLine("Already used context");
        continue;
      }

      // add the context to the used contexts list
      usedContexts.Add(context);

      // getting the user with the google id
      UserBase? user =
          await context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);

      if (user != null)
        return user;
    }

    return null;
  }

  // method to check if the user id is unique across all databases
  public async Task<bool> IsGoogleIdUnique(string googleId) {
    // creating a list for storing the already used contexts
    var usedContexts = new List<IdentityBaseContext>();

    // looping over all the user types and getting the db contexts for each
    foreach (UserTypes userType in Enum.GetValues(typeof(UserTypes))) {
      // getting the context for the user type
      IdentityBaseContext? context = _iidbContextFactory.GetContext(userType);

      // if the context is null, we skip it
      if (context == null)
        continue;

      // if the context is already used, we skip it
      if (usedContexts.Contains(context)) {
        Console.WriteLine("Already used context");
        continue;
      }

      // add the context to the used contexts list
      usedContexts.Add(context);

      // checking if the google id is already in use
      bool isUsed = await context.Users.AnyAsync(u => u.GoogleId == googleId);

      if (isUsed)
        return false;
    }

    return true;
  }
}
