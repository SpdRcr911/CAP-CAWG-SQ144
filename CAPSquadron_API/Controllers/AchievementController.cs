using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AchievementController : ControllerBase
{
    private readonly IXlsxParsingService _xlsxParsingService;
    private readonly IRetrieveDataService<Achievement> _retrieveDataService;
    private readonly IProcessDataService<AchievementCsvModel> _processDataService;

    /// <include file='CAPSquadron_API.xml' path='docs/achievement/controller/*'/>
    public AchievementController(IXlsxParsingService xlsxParsingService, IRetrieveDataService<Achievement> retrieveDataService, IProcessDataService<AchievementCsvModel> processDataService)
    {
        _xlsxParsingService = xlsxParsingService;
        _retrieveDataService = retrieveDataService;
        _processDataService = processDataService;
    }


    /// <include file='CAPSquadron_API.xml' path='docs/achievement/getAchievements/*'/>
    [HttpGet(Name = nameof(GetAchievements))]
    [ProducesResponseType<List<Achievement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAchievements()
    {
        var members = await _retrieveDataService.GetAsync();
        return Ok(members);
    }

    /// <include file='CAPSquadron_API.xml' path='docs/achievement/uploadFile/*'/>
    [HttpPost("upload", Name = nameof(UploadAchievementFile))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadAchievementFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var achievementsCsv = _xlsxParsingService.ParseXlsx<AchievementCsvModel>(file.OpenReadStream());
        await _processDataService.ProcessAsync(achievementsCsv);

        return Ok(new { success = "File successfully uploaded and processed" });
    }
}