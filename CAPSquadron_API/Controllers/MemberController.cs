// Controllers/MemberController.cs
using Microsoft.AspNetCore.Mvc;
using CAPSquadron_API.Services;
using CAPSquadron_API.Models;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController(ICsvParsingService csvParsingService, IRetrieveDataService<Member> retrieveDataService, IProcessDataService<MemberCsvModel> processDataService) : ControllerBase
{
    private readonly ICsvParsingService _csvParsingService = csvParsingService;
    private readonly IRetrieveDataService<Member> _retrieveDataService = retrieveDataService;
    private readonly IProcessDataService<MemberCsvModel> _processDataService = processDataService;

    /// <summary>
    /// Gets the list of all members.
    /// </summary>
    /// <returns>A list of members.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _retrieveDataService.GetAsync();
        return Ok(members);
    }

    /// <summary>
    /// Uploads a CSV file to update member data. bab
    /// eService report found in 'Personnel' > 'Attendance Log' > 'Attendance Sign-Sheet (Blank)' > SELECT: Unit, Member Type ALL, Report Format CSV
    /// </summary>
    /// <param name="file">The CSV file containing member data.</param>
    /// <returns>A success message if the file is processed correctly.</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
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