using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data.Databases;
using webapi.Models.Accounts;
using webapi.Models.Dto;

namespace webapi.Controllers;

[ApiController]
[Route("disabilityexpert")]
[Authorize("CompanyApproved")]
public class DisabilityExpertController : ControllerBase {
  private readonly DisabilityExpertGuardiansContext _expertGuardiansContext;

  public DisabilityExpertController(
      DisabilityExpertGuardiansContext expertGuardiansContext) {
    _expertGuardiansContext = expertGuardiansContext;
  }

  [HttpGet("all")]
  public async Task<IActionResult> GetAllDisabilityExperts() {
    List<DisabilityExpertDto> disabilityExperts =
        await _expertGuardiansContext.DisabilityExperts
            .Select(de => new DisabilityExpertDto { DisabilityExpertId = de.Id,
                                                    FirstName = de.FirstName,
                                                    LastName = de.LastName })
            .ToListAsync();

    return Ok(disabilityExperts);
  }
}
