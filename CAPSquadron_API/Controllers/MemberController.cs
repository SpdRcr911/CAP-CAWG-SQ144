using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController(ICsvParsingService csvParsingService, IRetrieveDataService<Member> retrieveDataService, IProcessDataService<MemberCsv> processDataService) : ControllerBase
{
    [HttpGet(Name = nameof(GetMembers))]
    [ProducesResponseType<List<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembers()
    {
        var membership = await retrieveDataService.GetAsync();
        return Ok(membership);
    }

    [HttpGet("{capid}", Name = nameof(GetMember))]
    [ProducesResponseType<Member>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMember(int capid)
    {
        var membership = await retrieveDataService.GetAsync(capid);
        return Ok(membership);
    }

    [HttpPost("capids", Name = nameof(GetMembersByCapids))]
    [ProducesResponseType<IEnumerable<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembersByCapids([FromBody] IEnumerable<int> capids)
    {
        var membership = await retrieveDataService.GetAsync(capids);
        return Ok(membership);
    }


    [HttpPost("upload", Name = nameof(UploadMembersList))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadMembersList(IFormFile file)
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
