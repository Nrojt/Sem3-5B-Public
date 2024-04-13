using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webapi.Helpers.Enums;
using webapi.Models.Authentication;

namespace webapi.Models.Accounts;

// super class for all users
public class UserBase : IdentityUser {
  [Required]
  public virtual UserTypes UserType { get; set; }

  public RefreshToken? RefreshToken { get; set; }

  public string? GoogleId { get; set; }
}
