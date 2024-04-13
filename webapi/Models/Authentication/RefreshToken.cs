using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Authentication;

public class RefreshToken {
  [Key]
  public int Id { get; set; }
  [Required]
  public string Token { get; set; } = null!;

  [Required]
  public bool IsRevoked { get; set; } = false;

  public DateTime LastModified { get; set; } = DateTime.Now;
  public DateTime Expires { get; set; }
}
