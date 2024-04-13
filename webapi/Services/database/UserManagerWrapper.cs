using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;

namespace webapi.Services.database;

// class used to wrap the UserManager, which allows us to use the UserManager
// with all user types If it just used the UserManager<UserBase>, it would not
// be able to use the methods of the specific user types (contravarience)
public class UserManagerWrapper<T> : IUserManagerWrapper
    where T : UserBase {
  private readonly UserManager<T> _userManager;

  public UserManagerWrapper(UserManager<T> userManager) {
    _userManager = userManager;
  }

  public async Task<UserBase?> FindByEmailAsync(string email) {
    return await _userManager.FindByEmailAsync(email);
  }

  public async Task<bool> CheckPasswordAsync(UserBase user, string password) {
    return await _userManager.CheckPasswordAsync((T)user, password);
  }

  public async Task UpdateAsync(UserBase? user) {
    await _userManager.UpdateAsync((T)user);
  }

  public async Task<IList<Claim>> GetClaimsAsync(UserBase user) {
    return await _userManager.GetClaimsAsync((T)user);
  }
}
