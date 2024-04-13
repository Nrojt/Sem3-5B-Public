using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.ApiModels.Research;
using webapi.Data.Databases;
using webapi.Models;
using webapi.Models.Accounts;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResearchController : ControllerBase {
  private readonly CompanyResearchContext _companyResearchcontext;
  private readonly DisabilityExpertGuardiansContext
      _disabilityExpertGuardiansContext;

  public ResearchController(
      CompanyResearchContext companyResearchcontext,
      DisabilityExpertGuardiansContext disabilityExpertGuardiansContext) {
    _companyResearchcontext = companyResearchcontext;
    _disabilityExpertGuardiansContext = disabilityExpertGuardiansContext;
  }

  // GET: api/Research
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Research>>> GetResearches() {

    var result =
        await _companyResearchcontext.Researches.Include(r => r.Company)
            .ToListAsync();

    List<ResearchModel> responseModel = new();

    foreach (var research in result) {
      responseModel.Add(new ResearchModel() {
        ResearchId = research.ResearchId, Title = research.Title,
        Disabilities = research.Disabilities, AgeRange = research.AgeRange,
        Description = research.Description, Date = research.Date,
        Location = research.Location, Reward = research.Reward,
        ResearchType = research.ResearchType,
        CompanyName = research.Company.CompanyName
      });
    }

    return Ok(responseModel);
  }

  // GET: api/Research/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Research>> GetResearch(int id) {
    var research =
        await _companyResearchcontext.Researches.Include(r => r.Company)
            .FirstOrDefaultAsync(r => r.ResearchId == id);

    if (research == null) {
      return NotFound();
    }
    ResearchModel responseModel =
        new() { ResearchId = research.ResearchId,
                Title = research.Title,
                Disabilities = research.Disabilities,
                AgeRange = research.AgeRange,
                Description = research.Description,
                Date = research.Date,
                Location = research.Location,
                Reward = research.Reward,
                ResearchType = research.ResearchType,
                CompanyName = research.Company.CompanyName };

    return Ok(responseModel);
  }

  // POST: api/Research
  [HttpPost]
  [Authorize(Policy = "CompanyApproved")]
  public async Task<IActionResult>
  CreateResearch([FromBody] ResearchModel researchModel) {
    // getting the email from the claims
    var emailClaim =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
    if (emailClaim == null)
      return BadRequest("No email claim found");

    string email = emailClaim.Value;

    // getting the company from the database
    Company? company =
        await _companyResearchcontext.Companies.FirstOrDefaultAsync(
            c => c.Email == email);

    // checking if the company exists
    if (company == null) {
      return BadRequest("No company found with the email: " + email);
    }

    // Checking if the disabilities and disability experts exist
    researchModel.Disabilities?.Select(disabilityDto => {
      var disability = _disabilityExpertGuardiansContext.Disabilities.Find(
          disabilityDto.DisabilityId);
      if (disability == null) {
        throw new ArgumentException("Invalid disability id: " +
                                    disabilityDto.DisabilityId);
      }
      return disability;
    });

    researchModel.DisabilityExperts?.Select(disabilityExpertDto => {
      var disabilityExpert =
          _disabilityExpertGuardiansContext.DisabilityExperts.Find(
              disabilityExpertDto.DisabilityExpertId);
      if (disabilityExpert == null) {
        throw new ArgumentException("Invalid disability expert id: " +
                                    disabilityExpertDto.DisabilityExpertId);
      }

      return disabilityExpert;
    });

    Research research = new() {
      Title = researchModel.Title,
      Disabilities = researchModel.Disabilities,
      AgeRange = researchModel.AgeRange,
      Description = researchModel.Description,
      Date = researchModel.Date,
      Location = researchModel.Location,
      Reward = researchModel.Reward,
      ResearchType = researchModel.ResearchType,
      DisabilityExperts = researchModel.DisabilityExperts,
      Company = company,
    };

    // adding the research to the database
    _companyResearchcontext.Researches.Add(research);
    await _companyResearchcontext.SaveChangesAsync();

    return Ok("Research created succesfully");
  }

  // PUT: api/Research/5
  [HttpPut("{id}"), Authorize(Policy = "CompanyApproved")]
  public async Task<IActionResult>
  UpdateResearch(int id, [FromBody] ResearchModel researchModel) {

    // getting the email from the claims
    var email =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
            ?.Value;
    if (email == null) {
      return BadRequest("No email found in the claims");
    }

    // getting the company from the database
    Company? company =
        await _companyResearchcontext.Companies.FirstOrDefaultAsync(
            c => c.Email == email);

    // checking if the company exists
    if (company == null) {
      return BadRequest("No company found with the email: " + email);
    }

    // getting the research from the database by id
    Research? research =
        await _companyResearchcontext.Researches.Include(r => r.Company)
            .FirstOrDefaultAsync(r => r.ResearchId == id);

    // check if the company is the owner of the research
    if (company.Id != research.Company.Id) {
      return Unauthorized("The company is not the owner of the research");
    }

    // setting the new values
    research.Title = researchModel.Title;
    research.Disabilities = researchModel.Disabilities;
    research.AgeRange = researchModel.AgeRange;
    research.Description = researchModel.Description;
    research.Date = researchModel.Date;
    research.Location = researchModel.Location;
    research.Reward = researchModel.Reward;
    research.ResearchType = researchModel.ResearchType;

    _companyResearchcontext.Researches.Update(research);
    await _companyResearchcontext.SaveChangesAsync();

    return Ok("Research updated succesfully");
  }
}
