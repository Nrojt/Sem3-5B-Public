using Microsoft.AspNetCore.Identity;
using Moq;

namespace webapi_specflow_tests.Helpers;

// Helperclass for mocking the UserManager
public static class MockUserManagers {
  // from
  // https://github.com/dotnet/aspnetcore/blob/main/src/Identity/test/Shared/MockHelpers.cs
  public static Mock<UserManager<TUser>> MockUserManager<TUser>()
      where TUser : class {
    var store = new Mock<IUserStore<TUser>>();
    var mgr = new Mock<UserManager<TUser>>(
        store.Object, null!, null!, null!, null!, null!, null!, null!,
        null!); // suppressing nullable warnings
    mgr.Object.UserValidators.Add(new UserValidator<TUser>());
    mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
    return mgr;
  }
}
