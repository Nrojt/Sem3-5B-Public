using webapi.Data.Bases;
using webapi.Helpers.Enums;

namespace webapi.Helpers.Interfaces;

public interface IIdbContextFactory {
  IdentityBaseContext? GetContext(UserTypes? userType);
}
