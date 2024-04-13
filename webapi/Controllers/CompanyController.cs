using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data.Databases;
using webapi.Models.Dto;

namespace webapi.Controllers;
[ApiController]
[Route("company")]
public class CompanyController : ControllerBase {
  private readonly CompanyResearchContext _companyResearchContext;

  public CompanyController(CompanyResearchContext companyResearchContext) {
    _companyResearchContext = companyResearchContext;
  }

  [HttpGet("all")]
  [Authorize("DisabilityExpert")]
  public async Task<IActionResult> GetAllCompanies() {
    List<CompanyDto> companyDtos =
        await _companyResearchContext.Companies
            .Select(c => new CompanyDto { CompanyId = c.Id,
                                          CompanyName = c.CompanyName })
            .ToListAsync();
    return Ok(companyDtos);
  }
}
