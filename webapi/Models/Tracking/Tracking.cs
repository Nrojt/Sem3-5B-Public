using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models.Accounts;

namespace webapi.Models.Tracking;

public class Tracking {
  // Primary Key
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  // required tracking information
  [Required]
  public string EventName { get; set; }
  [Required]
  public string Category { get; set; }
  [Required]
  public string Action { get; set; }
  [Required]
  public string Label { get; set; }

  // further information about tracking
  public DateTime? TimeStamp { get; set; } = DateTime.Now;
  [Required]
  public string UserId { get; set; } = null!;

  // Foreign Key
  [Required]
  public Company Company { get; set; } = null!;
}
