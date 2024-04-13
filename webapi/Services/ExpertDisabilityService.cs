using webapi.Models.Accounts;
using webapi.Models.Disabilities;

namespace webapi.Services;

public class ExpertDisabilityService {
  // method of checking if a disability expert has a disability and if they
  // exist
  public (bool succes, string message)
      CheckDisabilityExpertAndDisability(DisabilityExpert? disabilityExpert,
                                         Disability? disability,
                                         bool shouldHaveDisability) {
    // check if the disability exists
    if (disability == null) {
      return (false, "Disability does not exist");
    }

    // check if the disability expert exists
    if (disabilityExpert == null) {
      return (false, "Disability expert does not exist");
    }

    // check if the disability expert already has the disability
    bool hasDisability = disabilityExpert.ExpertDisabilities.Exists(
        ed => ed.DisabilityId == disability.DisabilityId);

    if (hasDisability == shouldHaveDisability) {
      return (
          true,
          $"Disability expert has {shouldHaveDisability} for disability, and method has {hasDisability}");
    }

    return (
        false,
        $"Disability expert has {shouldHaveDisability} for disability, and method has {hasDisability}");
  }
}
