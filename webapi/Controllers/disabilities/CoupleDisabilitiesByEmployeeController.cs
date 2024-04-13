using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.ApiModels.Disabilities;
using webapi.Data.Databases;
using webapi.Models.Accounts;
using webapi.Models.Disabilities;
using webapi.Services;

namespace webapi.Controllers.disabilities;

// This class will manage adding and removing disabilities from a
// DisabilityExpert
[ApiController]
[Route("disability")]
[Authorize(Policy = "Employee")]
public class CoupleDisabilitiesByEmployeeController : ControllerBase {
  private readonly DisabilityExpertGuardiansContext
      _disabilityExpertGuardiansContext;
  private readonly ExpertDisabilityService _expertDisabilityService;

  // constructor
  public CoupleDisabilitiesByEmployeeController(
      DisabilityExpertGuardiansContext disabilityExpertGuardiansContext,
      ExpertDisabilityService expertDisabilityService) {
    _disabilityExpertGuardiansContext = disabilityExpertGuardiansContext;
    _expertDisabilityService = expertDisabilityService;
  }

  // endpoint for adding a disability to a disability expert by id, only for
  // employees
  [HttpPost("addToDisabilityExpertByEmployee")]
  [Authorize(Policy = "Employee")]
  public async Task<IActionResult> AddDisabilityToDisabilityExpertByEmployee([
    FromBody
  ] CoupleDisabilityByEmployeeModel coupleDisabilityByEmployeeModel) {

    // get the disability expert and disability from the database, tuple return
    var resultTuple = await GetDisabilityExpertAndDisability(
        coupleDisabilityByEmployeeModel.DisabilityExpertId,
        coupleDisabilityByEmployeeModel.DisabilityId);

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

    Console.WriteLine("Disability expert has " +
                      disabilityExpert.ExpertDisabilities.Count +
                      " disabilities");

    // add the disability to the disability expert
    _disabilityExpertGuardiansContext.ExpertDisabilities.Add(expertDisability);

    // save changes to persist the new disability in the database
    await _disabilityExpertGuardiansContext.SaveChangesAsync();

    return Ok("Disability added to disability expert");
  }

  // endpoint for removing a disability from a disability expert by id, only for
  // employees
  [HttpDelete("removeFromDisabilityExpertByEmployee")]
  [Authorize(Policy = "Employee")]
  public async Task<IActionResult>
  RemoveDisabilityFromDisabilityExpertByEmployee([
    FromBody
  ] CoupleDisabilityByEmployeeModel coupleDisabilityByEmployeeModel) {
    // get the disability and the disability expert from the database, tuple
    // return
    var returnTuple = await GetDisabilityExpertAndDisability(
        coupleDisabilityByEmployeeModel.DisabilityExpertId,
        coupleDisabilityByEmployeeModel.DisabilityId);

    Disability? disability = returnTuple.Item2;

    DisabilityExpert? disabilityExpert = returnTuple.Item1;

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
        string disabilityExpertId, int disabilityId)
    {
      Console.WriteLine("Finding disability " + disabilityId +
                        " and disability expert " + disabilityExpertId + "...");

      // get the disability and the disability expert from the database
      Disability? disability =
          await _disabilityExpertGuardiansContext.Disabilities
              .FirstOrDefaultAsync(d => d.DisabilityId == disabilityId);

      DisabilityExpert? disabilityExpert =
          await _disabilityExpertGuardiansContext.DisabilityExperts
              .Include(de => de.ExpertDisabilities) // Eagerly load the
              // ExpertDisabilities, only
              // for the disability expert
              .FirstOrDefaultAsync(de => de.Id == disabilityExpertId);

      Console.WriteLine("Search complete");

      // return the disability expert and the disability
      return (disabilityExpert, disability);
    }
}
