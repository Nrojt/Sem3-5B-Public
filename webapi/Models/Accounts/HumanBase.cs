using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Accounts;

public abstract class HumanBase : UserBase {
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string? PostalCode { get; set; }

  [Required]
  public int BirthYear {
    get; set;
  }
}
