using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController(ICsvParsingService csvParsingService, IRetrieveDataService<Member> retrieveDataService, IProcessDataService<MemberCsv> processDataService) : ControllerBase
{
    [HttpGet(Name = nameof(GetMembershipIns))]
    [ProducesResponseType<List<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembershipIns()
    {
        var membership = await retrieveDataService.GetAsync();
        return Ok(membership);
    }

    [HttpGet("{capid}", Name = nameof(GetMembershipIn))]
    [ProducesResponseType<Member>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembershipIn(int capid)
    {
        var membership = await retrieveDataService.GetAsync(capid);
        return Ok(membership);
    }

    [HttpPost("capids", Name = nameof(GetMembershipInsByCapids))]
    [ProducesResponseType<IEnumerable<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembershipInsByCapids([FromBody] IEnumerable<int> capids)
    {
        var membership = await retrieveDataService.GetAsync(capids);
        return Ok(membership);
    }


    [HttpPost("upload", Name = nameof(UploadMembershipList))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadMembershipList(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        try
        {
            var membershipCsv = csvParsingService.ParseCsv<MemberCsv>(file.OpenReadStream());
            await processDataService.ProcessAsync(membershipCsv);
            return Ok("Membership list updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }
}
