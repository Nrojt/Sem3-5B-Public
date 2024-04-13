using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.ApiModels.Tracking;
using webapi.Data.Databases;
using webapi.Helpers.Enums;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.userresolvers;

namespace webapi.Controllers.Tracking;

// This class will manage adding and removing tracking information
[ApiController]
[Route("tracking")]
[Authorize(Policy = "CompanyApproved")]
public class TrackingController : ControllerBase {
  private readonly CompanyResearchContext _companyResearchContext;
  private readonly UserResolveService _userResolveService;

  public TrackingController(CompanyResearchContext companyResearchContext,
                            UserResolveService userResolveService) {
    _companyResearchContext = companyResearchContext;
    _userResolveService = userResolveService;
  }

  // endpoint for adding tracking information
  [HttpPost("addTracking")]
  public async Task<IActionResult>
  AddTracking([FromBody] TrackingModel trackingModel) {
    // get the company from the database
    Company? company = await GetCompanyFromClaims();

    // check if the company exists
    if (company == null) {
      return BadRequest("Company does not exist");
    }

    // create the tracking instance
    Models.Tracking.Tracking tracking =
        new Models.Tracking.Tracking { EventName = trackingModel.EventName,
                                       Category = trackingModel.Category,
                                       Action = trackingModel.Action,
                                       Label = trackingModel.Label,
                                       TimeStamp = trackingModel.TimeStamp,
                                       UserId = trackingModel.UserId,
                                       Company = company };

    // add the tracking to the database
    await _companyResearchContext.Tracking.AddAsync(tracking);
    await _companyResearchContext.SaveChangesAsync();

    // Create an anonymous object for the JSON response
    var jsonResponse = new { trackingId = tracking.Id };

    return Ok(jsonResponse);
  }

  // endpoint for removing tracking information
  [HttpDelete("removeTracking")]
  public async Task<IActionResult> RemoveTracking([FromBody] int trackingId) {
    // get the company from the database
    Company? company = await GetCompanyFromClaims();

    // check if the company exists
    if (company == null) {
      return BadRequest("Company does not exist");
    }

    // check if the trackingid exist
    Models.Tracking.Tracking? tracking =
        await _companyResearchContext.Tracking.Include(t => t.Company)
            .FirstOrDefaultAsync(t => t.Id == trackingId);

    if (tracking == null) {
      return BadRequest("Tracking does not exist");
    }

    // Check if this company owns this tracking
    if (company.Id == tracking.Company.Id) {
      // deleting the tracking from the database
      _companyResearchContext.Tracking.Remove(tracking);
      await _companyResearchContext.SaveChangesAsync();

      return Ok("Tracking removed");
    }

    // If not, return Forbidden with a custom message
    return new ContentResult {
      StatusCode = 403,
      Content = "You not authorized to access this tracking resource.",
      ContentType = "text/plain"
    };
  }

  private async Task<Company?> GetCompanyFromClaims() {
    // getting the email of the user from claims
    string? email =
        HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
            ?.Value;

    if (email.IsNullOrEmpty()) {
      return null;
    }

    // get the company from the database
    Company? company = (Company)await _userResolveService.GetUserByEmailAsync(
        UserTypes.CompanyApproved, email);

    return company;
  }

  // updating tracking data does not really make sense, so I will not implement
  // it. Feel free to implement it yourself
}
