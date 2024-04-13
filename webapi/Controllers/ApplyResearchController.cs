using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data.Databases;
using webapi.Models;
using webapi.Models.Dto; // For IsNullOrEmpty extension method

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplyResearchController : ControllerBase {
  private readonly CompanyResearchContext _companyResearchcontext;

  public ApplyResearchController(
      CompanyResearchContext companyResearchcontext) {
    _companyResearchcontext = companyResearchcontext;
  }

  [HttpPut, Authorize(Policy = "DisabilityExpert")]
  public async Task<ActionResult> ApplyResearch([FromQuery] int researchId) {

    Console.WriteLine("ResearchId " + researchId);

    // extract the disability expert id from the claims
    var disabilityExpertClaim = HttpContext.User.Claims.FirstOrDefault(
        c => c.Type == ClaimTypes.NameIdentifier);

    if (disabilityExpertClaim == null ||
        string.IsNullOrEmpty(disabilityExpertClaim.Value)) {
      return BadRequest("Disability expert id is null or empty");
    }

    string? disabilityExpertId = disabilityExpertClaim.Value;

    Console.WriteLine("DisabilityExpertId " + disabilityExpertId);

    try {
      Research? research =
          await _companyResearchcontext.Researches
              .Include(r => r.DisabilityExperts)
              .FirstOrDefaultAsync(r => r.ResearchId == researchId);

      if (research == null) {
        return NotFound("Research not found with id " + researchId);
      }

      // TODO check if the disability expert is already applied to the research
      if (research.DisabilityExperts.Any(de => de.DisabilityExpertId ==
                                               disabilityExpertId)) {
        return BadRequest("Disability expert already applied to research");
      }

      DisabilityExpertDto disabilityExpertDto =
          new DisabilityExpertDto() { DisabilityExpertId = disabilityExpertId };

      research.DisabilityExperts.Add(disabilityExpertDto);

      await _companyResearchcontext.SaveChangesAsync();

      return Ok("DisabilityExpert applied to research");
    } catch (Exception ex) {
      return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Internal Server Error: {ex.Message}");
    }
  }
}
