using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data.Databases;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;

namespace webapi.Controllers.disabilities;

[ApiController]
[Route("disability")]
public class GetDisabilitiesFromDisabilityExpertController : ControllerBase {

  private readonly DisabilityExpertGuardiansContext
      _disabilityExpertGuardiansContext;

  public GetDisabilitiesFromDisabilityExpertController(
      DisabilityExpertGuardiansContext disabilityExpertGuardiansContext) {
    _disabilityExpertGuardiansContext = disabilityExpertGuardiansContext;
  }

  // method for getting all disabilities from a disability expert by id, only
  // for disability experts
  [HttpGet("getAllDisabilitiesFromDisabilityExpert")]
  [Authorize(Policy = "DisabilityExpert")]
  public async Task<IActionResult> GetAllDisabilitiesFromDisabilityExpert() {
    // get the email from claims
    String? email = User.FindFirst(ClaimTypes.Email)?.Value;

    if (string.IsNullOrEmpty(email)) {
      return BadRequest("Email is null or empty");
    }

    // get the disability expert from the database
    DisabilityExpert? disabilityExpert =
        await _disabilityExpertGuardiansContext.DisabilityExperts
            .FirstOrDefaultAsync(de => de.Email == email);

    // check if the disability expert exists
    if (disabilityExpert == null) {
      return NotFound("Disability expert not found");
    }

    return await DisabilitiesFound(disabilityExpert);
  }

  // method for getting all disabilities from a disability expert by id, only
  // for employees
  [HttpGet("getAllDisabilitiesFromDisabilityExpertByEmployee")]
  [Authorize(Policy = "Employee")]
  public async Task<IActionResult>
  GetAllDisabilitiesFromDisabilityExpertByEmployee(
      [FromQuery] string disabilityExpertId) {
    // get the disability expert from the database
    DisabilityExpert? disabilityExpert =
        await _disabilityExpertGuardiansContext.DisabilityExperts
            .FirstOrDefaultAsync(de => de.Id == disabilityExpertId);

    // check if the disability expert exists
    if (disabilityExpert == null) {
      return NotFound("Disability expert not found");
    }

    return await DisabilitiesFound(disabilityExpert);
  }

  // method for getting all disabilities from a disability expert by id, only
  // for employees
  private async Task<List<Disability>>
  GetDisabilitiesFromDisabilityExpert(DisabilityExpert disabilityExpert) {
    // return all disabilities from the disability expert
    List<Disability> disabilities =
        await _disabilityExpertGuardiansContext.ExpertDisabilities
            .Where(ed => ed.DisabilityExpertId == disabilityExpert.Id)
            .Join(_disabilityExpertGuardiansContext.Disabilities,
                  ed => ed.DisabilityId, d => d.DisabilityId, (ed, d) => d)
            .ToListAsync();

    return disabilities;
  }

  private async Task<IActionResult>
  DisabilitiesFound(DisabilityExpert disabilityExpert) {
    // return all disabilities of the disability expert
    List<Disability> disabilities =
        await GetDisabilitiesFromDisabilityExpert(disabilityExpert);

    // if the list is null or empty, return not found
    if (disabilities.Count == 0) {
      return NotFound("No disabilities found");
    }

    return Ok(disabilities);
  }
}
