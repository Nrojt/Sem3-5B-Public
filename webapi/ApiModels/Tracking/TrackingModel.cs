using System.ComponentModel.DataAnnotations;

namespace webapi.ApiModels.Tracking;

public class TrackingModel {
  [Required]
  public string EventName { get; set; } = null!;
  [Required]
  public string Category { get; set; } = null!;
  [Required]
  public string Action { get; set; } = null!;
  [Required]
  public string Label { get; set; } = null!;

  // this is nullable because we want to be able to set the timestamp to null.
  public DateTime? TimeStamp { get; set; }

  [Required]
  public string UserId { get; set; } = null!;
}
