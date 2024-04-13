using Microsoft.AspNetCore.Authorization;

namespace webapi.Helpers;

[AttributeUsage(AttributeTargets.Method, Inherited = false,
                AllowMultiple = false)]
public class UserTypeAuthorizationAttribute : AuthorizeAttribute {
  public UserTypeAuthorizationAttribute(params string[] allowedPolicies) {
    if (allowedPolicies == null || allowedPolicies.Length == 0) {
      throw new ArgumentException("At least one policy must be specified.",
                                  nameof(allowedPolicies));
    }

    Policies = allowedPolicies;
  }

  public string[] Policies { get; }
}
