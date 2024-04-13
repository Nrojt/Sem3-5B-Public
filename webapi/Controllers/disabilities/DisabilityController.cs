using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.ApiModels.Disabilities;
using webapi.Data.Databases;
using webapi.Models.Disabilities;

namespace webapi.Controllers.disabilities;

[ApiController]
[Route("disability")]
// allowing employees and disability experts to access this controller
[Authorize(Policy = "EmployeeOrDisabilityExpert")]
public class DisabilityController : ControllerBase {
  private readonly DisabilityExpertGuardiansContext _disabilityContext;

  // constructor
  public DisabilityController(
      DisabilityExpertGuardiansContext disabilityContext) {
    _disabilityContext = disabilityContext;
  }

  // endpoint for creating a new disability
  [HttpPost("create")]
  public async Task<IActionResult>
  CreateDisability([FromBody] DisabilityModel disabilityModel) {
    // normalize the name of the disability
    string nameNormalized = disabilityModel.DisabilityName.ToUpper();

    // check if the disability already exists
    if (_disabilityContext.Disabilities.Any(
            disability =>
                disability.DisabilityNameNormalized == nameNormalized)) {
      return BadRequest("Disability already exists");
    }

    // creating the new disability
    Disability disability =
        new() { DisabilityName = disabilityModel.DisabilityName,
                DisabilityNameNormalized = nameNormalized,
                DisabilityDescription = disabilityModel.DisabilityDescription,
                Language = disabilityModel.Language };

    _disabilityContext.Disabilities.Add(disability);

    // Save changes to persist the new disability in the database
    await _disabilityContext.SaveChangesAsync();

    return Ok("Disability created");
  }

  // endpoint for updating a disability
  [HttpPut("update")]
  [Authorize(Policy = "Employee")]
  public async Task<IActionResult>
  UpdateDisability([FromBody] Disability disability) {
    // normalize the name of the disability, for the case when the name is
    // updated
    disability.DisabilityNameNormalized = disability.DisabilityName.ToUpper();

    // update the disability
    var operation = _disabilityContext.Disabilities.Update(disability);

    // check if the disability was updated
    if (operation.State == EntityState.Modified) {
      // Save changes to persist the update in the database
      await _disabilityContext.SaveChangesAsync();

      return Ok("Disability updated");
    }

    // If the disability was not in the Modified state, it means it wasn't found
    return NotFound("Disability not found");
  }

  // endpoint for deleting a disability
  [HttpDelete("delete")]
  [Authorize(Policy = "Employee")]
  public async Task<IActionResult>
  DeleteDisability([FromQuery] int disabilityId) {
    // Find the disability by ID
    var disabilityToDelete =
        await _disabilityContext.Disabilities.FindAsync(disabilityId);

    // Check if the disability exists
    if (disabilityToDelete != null) {
      // delete the disability from the database
      var operation =
          _disabilityContext.Disabilities.Remove(disabilityToDelete);

      // check if the disability was deleted
      if (operation.State == EntityState.Deleted) {
        // Save changes to persist the deletion in the database
        await _disabilityContext.SaveChangesAsync();

        return Ok("Disability deleted");
      }
    }

    // If the disability was not in the Deleted state, it means it wasn't found
    return NotFound("Disability not found");
  }

  // endpoint for getting all disabilities
  [HttpGet("getall")]
  public async Task<IActionResult> GetAllDisabilities() {
    // return all disabilities
    List<Disability>? disabilities = _disabilityContext.Disabilities.ToList();

    // if the list is null or empty, return not found
    if (disabilities.Count == 0) {
      return NotFound("No disabilities found");
    }
    return Ok(disabilities);
  }

  // endpoint for getting a disability by id
  [HttpGet("getbyid")]
  public async Task<IActionResult>
  GetDisabilityById([FromQuery] int disabilityId) {
    // find the disability with the given id
    Disability? disability =
        await _disabilityContext.Disabilities.FindAsync(disabilityId);

    if (disability == null) {
      return NotFound("Disability not found");
    }

    return Ok(disability);
  }
}
