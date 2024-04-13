using System.Security.Claims;
using webapi.Models.Accounts;

namespace webapi.Helpers.Interfaces;

// Wrapper for the UserManager, which allows us to use the UserManager with all
// user types
public interface IUserManagerWrapper {
  // defining the methods that are used in the UserResolveService (or any other
  // class that ends up using this interface)
  Task<UserBase?> FindByEmailAsync(string email);
  Task<bool> CheckPasswordAsync(UserBase user, string password);
  Task UpdateAsync(UserBase? user);
  Task<IList<Claim>> GetClaimsAsync(UserBase user);
}
