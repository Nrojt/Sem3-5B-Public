using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;

namespace webapi.Services.userresolvers;

public class UserResolveService {
  // Dictionary (key, value) where key is the type of user and value is the
  // manager for that type of user usermanager is provided by IdentityUser
  // framework and is used to manage users
  private readonly Dictionary<UserTypes, IUserManagerWrapper> _managers;

  private readonly IIdbContextFactory _iidbContextFactory;

  // importing the managers from program.cs
  public UserResolveService(Dictionary<UserTypes, IUserManagerWrapper> managers,
                            IIdbContextFactory iidbContextFactory) {
    _managers = managers ?? throw new ArgumentNullException(nameof(managers));
    _iidbContextFactory = iidbContextFactory;
  }

  // This method is used to get a user by email, checking all database contexts
  public async Task<UserBase?> GetUserByEmailAsync(string email) {
    // getting all distinct usermanagers from the managers
    // Group the dictionary values by their database context and select one
    // value from each group
    var distinctDatabaseContexts = _managers.Values.Distinct().ToList();

    UserBase? user = null;

    // looping through all database managers to check if the user exists
    foreach (var userManager in distinctDatabaseContexts) {
      Console.WriteLine("Using usermanager " + userManager);
      user = await userManager.FindByEmailAsync(email);
      if (user != null) {
        Console.WriteLine("user has been found");
        return user;
      }
    }

    Console.WriteLine("user not found");
    // return the first non-null user or null if none found
    return user;
  }

  // this method is used to get a user by email and usertype
  public async Task<UserBase?> GetUserByEmailAsync(UserTypes userType,
                                                   string email) {
    // Check which type of user we have and use the correct manager
    var userManager = _managers[userType];
    Console.WriteLine("Using usermanager for usertype " + userType +
                      " which is " + userManager);
    UserBase? user = await userManager.FindByEmailAsync(email);

    return user;
  }

  // This method is used to check if a password is correct
  public async Task<bool> CheckPasswordAsync(UserBase user, string password) {
    // Check which type of user we have and use the correct manager
    var userManager = _managers[user.UserType];

    // Check which type of user we have and use the correct manager
    bool result = await userManager.CheckPasswordAsync(user, password);
    return result;
  }

  public async Task UpdateAsync(UserBase? user) {
    // Check which type of user we have and use the correct manager
    var userManager = _managers[user?.UserType ??
                                throw new ArgumentNullException(nameof(user))];

    // Check which type of user we have and use the correct manager
    await userManager.UpdateAsync(user);
  }

  // method to check if the email is already in use by another user
  public async Task<bool> IsEmailInUseAsync(string email) {
    // looping over all the managers to check if the email is in use
    foreach (var userManager in _managers.Values) {
      var user = await userManager.FindByEmailAsync(email);
      if (user != null) {
        return true;
      }
    }
    return false;
  }

  // method for getting a user by refresh token
  public async Task<UserBase?> GetUserByRefreshTokenAsync(string refreshToken,
                                                          UserTypes userType) {
    var context = _iidbContextFactory.GetContext(userType);
    var user =
        await context.Users.Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.RefreshToken.Token == refreshToken);

    return user;
  }

  // method for getting all stored user claims
  public async Task<List<Claim>> GetUserClaimsAsync(UserBase user) {
    // Check which type of user we have and use the correct manager
    var userManager = _managers[user.UserType];

    // Check which type of user we have and use the correct manager
    var claims = await userManager.GetClaimsAsync(user);

    return claims.ToList();
  }
}
