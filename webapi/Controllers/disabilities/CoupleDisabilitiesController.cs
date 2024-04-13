using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Data.Databases;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;
using webapi.Services;

namespace webapi.Controllers.disabilities;

[ApiController]
[Route("disability")]
[Authorize(Policy = "DisabilityExpert")]
public class CoupleDisabilitiesController : ControllerBase {
  private readonly DisabilityExpertGuardiansContext
      _disabilityExpertGuardiansContext;
  private readonly ExpertDisabilityService _expertDisabilityService;

  // constructor
  public CoupleDisabilitiesController(
      DisabilityExpertGuardiansContext disabilityExpertGuardiansContext,
      ExpertDisabilityService expertDisabilityService) {
    _disabilityExpertGuardiansContext = disabilityExpertGuardiansContext;
    _expertDisabilityService = expertDisabilityService;
  }

  // endpoint for adding a disability to a guardian by id, only for disability
  // experts. Getting users by email from claim
  [HttpPost("addToDisabilityExpert")]
  public async Task<IActionResult>
  AddDisabilityToDisabilityExpert([FromBody] int disabilityId) {
    // get the email from claims
    String? email = User.FindFirst(ClaimTypes.Email)?.Value;

    if (email.IsNullOrEmpty()) {
      return BadRequest("Email is null or empty");
    }

    // get the disability expert and disability from the database, tuple return
    var resultTuple =
        await GetDisabilityExpertAndDisability(email, disabilityId);

    // getting the disability expert and disability from the tuple
    Disability? disability = resultTuple.Item2;

    DisabilityExpert? disabilityExpert = resultTuple.Item1;

    // check if the disability expert and the disability exist and if the
    // disability expert does not have the disability
    var checkResult =
        _expertDisabilityService.CheckDisabilityExpertAndDisability(
            disabilityExpert, disability, false);

    if (checkResult.succes == false) {
      return BadRequest(checkResult.message);
    }

    // create the ExpertDisability instance to represent the relationship
    ExpertDisability expertDisability =
        new ExpertDisability { DisabilityExpertId = disabilityExpert.Id,
                               DisabilityId = disability.DisabilityId };

    // add the disability to the disability expert
    disabilityExpert.ExpertDisabilities.Add(expertDisability);

    // save changes to persist the new disability in the database
    await _disabilityExpertGuardiansContext.SaveChangesAsync();

    return Ok("Disability added to disability expert");
  }

  // endpoint for removing a disability from a disability expert by id, only for
  // disability experts. Getting users by email from claim
  [HttpDelete("removeFromDisabilityExpert")]
  public async Task<IActionResult>
  RemoveDisabilityFromDisabilityExpert([FromBody] int disabilityId) {
    // get the email from claims
    String? email = User.FindFirst(ClaimTypes.Email)?.Value;

    if (email.IsNullOrEmpty()) {
      return BadRequest("Email is null or empty");
    }

    // get the disability expert and disability from the database, tuple return
    var resultTuple =
        await GetDisabilityExpertAndDisability(email, disabilityId);

    // getting the disability expert and disability from the tuple
    Disability? disability = resultTuple.Item2;

    DisabilityExpert? disabilityExpert = resultTuple.Item1;

    // check if the disability expert and the disability exist and if the
    // disability expert does not have the disability
    var checkResult =
        _expertDisabilityService.CheckDisabilityExpertAndDisability(
            disabilityExpert, disability, true);

    if (checkResult.succes == false) {
      return BadRequest(checkResult.message);
    }

    // Find the existing ExpertDisability instance in the collection
    ExpertDisability existingExpertDisability =
        disabilityExpert.ExpertDisabilities.FirstOrDefault(
            ed => ed.DisabilityId == disability.DisabilityId);

    if (existingExpertDisability == null) {
      return BadRequest("Disability expert does not have the disability");
    }

    // remove the disability from the disability expert
    _disabilityExpertGuardiansContext.ExpertDisabilities.Remove(
        existingExpertDisability);

    // save changes to persist the new disability in the database
    await _disabilityExpertGuardiansContext.SaveChangesAsync();

    return Ok("Disability removed from disability expert");
  }

  // method for awaiting the disability expert and the disability from the
  // database
    private async Task<(DisabilityExpert?, Disability?)> GetDisabilityExpertAndDisability(
        string disabilityExpertEmail, int disabilityId)
    {
      Console.WriteLine("Finding disability " + disabilityId +
                        " and disability expert " + disabilityExpertEmail +
                        "...");

      // get the disability and the disability expert from the database
      Disability? disability =
          await _disabilityExpertGuardiansContext.Disabilities
              .FirstOrDefaultAsync(d => d.DisabilityId == disabilityId);

      DisabilityExpert? disabilityExpert =
          await _disabilityExpertGuardiansContext.DisabilityExperts
              .Include(de => de.ExpertDisabilities) // Eagerly load the
              // ExpertDisabilities, only
              // for the disability expert
              .FirstOrDefaultAsync(de => de.NormalizedEmail ==
                                         disabilityExpertEmail.ToUpper());

      Console.WriteLine("Search complete");

      // return the disability expert and the disability
      return (disabilityExpert, disability);
    }
}
