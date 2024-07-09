// Controllers/MemberController.cs
using Microsoft.AspNetCore.Mvc;
using CAPSquadron_API.Services;
using CAPSquadron_API.Models;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class MemberController(ICsvParsingService csvParsingService, IRetrieveDataService<Member> retrieveDataService, IProcessDataService<MemberCsvModel> processDataService) : ControllerBase
{
    private readonly ICsvParsingService _csvParsingService = csvParsingService;
    private readonly IRetrieveDataService<Member> _retrieveDataService = retrieveDataService;
    private readonly IProcessDataService<MemberCsvModel> _processDataService = processDataService;

    /// <summary>
    /// Gets the list of all members.
    /// </summary>
    /// <returns>A list of members.</returns>
    [HttpGet(Name = nameof(GetMembers))]
    [ProducesResponseType<List<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _retrieveDataService.GetAsync();
        return Ok(members);
    }

    /// <summary>
    /// Gets a member.
    /// </summary>
    /// <returns>A member.</returns>
    [HttpGet("{capid}", Name = nameof(GetMember))]
    [ProducesResponseType<Member>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMember(int capid)
    {
        var members = await _retrieveDataService.GetAsync(capid);
        return Ok(members);
    }

    /// <summary>
    /// Gets a list of members from a list of CAPIDs.
    /// </summary>
    /// <returns>A list of members.</returns>
    [HttpPost("capids", Name = nameof(GetMembersByCapids))]
    [ProducesResponseType<IEnumerable<Member>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembersByCapids([FromBody] IEnumerable<int> capids)
    {
        var cadetTrackerData = await _retrieveDataService.GetAsync(capids);
        return Ok(cadetTrackerData);
    }

    /// <summary>
    /// Uploads a CSV file to update member data.
    /// </summary>
    /// <remarks>
    /// eService report found in 'Personnel' > 'Attendance Log' > 'Attendance Sign-Sheet (Blank)' > SELECT: Unit, Member Type ALL, Report Format CSV
    /// </remarks>
    /// <param name="file">The CSV file containing member data.</param>
    /// <returns>A success message if the file is processed correctly.</returns>
    [HttpPost("upload",Name = nameof(UploadMemberFile))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadMemberFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var membersCsv = _csvParsingService.ParseCsv<MemberCsvModel>(file.OpenReadStream());
        await _processDataService.ProcessAsync(membersCsv);

        return Ok(new { success = "File successfully uploaded and processed" });
    }
}