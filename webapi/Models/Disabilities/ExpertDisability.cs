using webapi.Models.Accounts;

namespace webapi.Models.Disabilities;

// Entity to represent the many-to-many relationship of the disability experts
// and disabilities
public class ExpertDisability {
  public string DisabilityExpertId { get; set; }
  public DisabilityExpert DisabilityExpert { get; set; }

  public int DisabilityId { get; set; }
  public Disability Disability { get; set; }
}
