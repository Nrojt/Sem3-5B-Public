using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.ApiModels.ProfilePage;
using webapi.Data.Databases;
using webapi.Models.Accounts;

namespace webapi.Controllers.profilepage;

[ApiController]
[Route("disabilityexpert")]
[Authorize("DisabilityExpert")]
public class GetDisabilityExpertController : ControllerBase {
  private readonly DisabilityExpertGuardiansContext _expertGuardiansContext;

  public GetDisabilityExpertController(
      DisabilityExpertGuardiansContext expertGuardiansContext) {
    _expertGuardiansContext = expertGuardiansContext;
  }

  [HttpGet]
  public async Task<IActionResult> GetDisabilityExpertById() {
    // Extract the email from the claims
    var emailClaim =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

    if (emailClaim.Value.IsNullOrEmpty()) {
      return BadRequest("Email is null or empty");
    }

    DisabilityExpert disabilityExpert =
        await GetDisabilityExpert(emailClaim.Value);

    if (disabilityExpert == null) {
      return BadRequest("Disability expert not found");
    }

    // Check if the disability expert exists
    if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value)) {
      return BadRequest("No email claim found");
    }

    DisabilityExpertModel responseModel =
        new() { FirstName = disabilityExpert.FirstName,
                LastName = disabilityExpert.LastName,
                Email = disabilityExpert.Email,
                BirthYear = disabilityExpert.BirthYear,
                PostalCode = disabilityExpert.PostalCode,
                DisabilityIds = disabilityExpert.ExpertDisabilities
                                    .Select(ed => ed.DisabilityId)
                                    .ToList(),
                GuardianId = disabilityExpert.Guardian?.Id,
                TypeBenadering = disabilityExpert.TypeBenadering };

    return Ok(responseModel);
  }

  private async Task<DisabilityExpert?>
  GetDisabilityExpert(string disabilityExpertEmail) {
    Console.WriteLine("Finding disability expert " + disabilityExpertEmail +
                      "...");

    // get the disability expert from the database

    DisabilityExpert? disabilityExpert =
        await _expertGuardiansContext.DisabilityExperts
            .Include(de => de.ExpertDisabilities) // Eagerly load the
            // ExpertDisabilities, only
            // for the disability expert
            .FirstOrDefaultAsync(de => de.NormalizedEmail ==
                                       disabilityExpertEmail.ToUpper());

    Console.WriteLine("Search complete");
    // return the disability expert
    return (disabilityExpert);
  }

  // PUT: api/disabilityexpert/
  [HttpPut]
  public async Task<IActionResult>
  UpdateResearch([FromBody] DisabilityExpertModel disabilityExpertModel) {

    // getting the email from the claims
    var email =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
            ?.Value;
    if (email == null) {
      return BadRequest("No email found in the claims");
    }

    // getting the user from the database
    DisabilityExpert? disabilityExpert =
        await _expertGuardiansContext.DisabilityExperts.Include(d => d.Guardian)
            .Include(d => d.ExpertDisabilities)
            .FirstOrDefaultAsync(d => d.Email == email);

    // checking if the user exists
    if (disabilityExpert == null) {
      return BadRequest("No company found with the email: " + email);
    }

    // setting the new values
    disabilityExpert.FirstName = disabilityExpertModel.FirstName;
    disabilityExpert.LastName = disabilityExpertModel.LastName;
    disabilityExpert.PostalCode = disabilityExpertModel.PostalCode;
    disabilityExpert.BirthYear = disabilityExpertModel.BirthYear;
    // disabilityExpert.Guardian = disabilityExpertModel.GuardianId;
    disabilityExpert.TypeBenadering = disabilityExpertModel.TypeBenadering;
    disabilityExpert.Email = disabilityExpertModel.Email;
    // disabilityExpert.ExpertDisabilities =
    // disabilityExpertModel.DisabilityIds;

    _expertGuardiansContext.DisabilityExperts.Update(disabilityExpert);
    await _expertGuardiansContext.SaveChangesAsync();

    return Ok("Disability Expert updated succesfully");
  }
}
