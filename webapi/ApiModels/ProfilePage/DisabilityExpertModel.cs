namespace webapi.ApiModels.ProfilePage;

// TODO change this to use disability and guardian dto's (couple id with name
// and email)
public class DisabilityExpertModel {
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public int BirthYear { get; set; }
  public string? PostalCode { get; set; }
  public List<int>? DisabilityIds { get; set; }
  public string? GuardianId { get; set; }
  public string? TypeBenadering { get; set; }
}
