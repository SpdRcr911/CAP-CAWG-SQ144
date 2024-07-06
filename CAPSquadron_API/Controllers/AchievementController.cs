using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AchievementController(IXlsxParsingService xlsxParsingService, IRetrieveDataService<Achievement> retrieveDataService, IProcessDataService<AchievementCsvModel> processDataService) : ControllerBase
{
    private readonly IXlsxParsingService _xlsxParsingService = xlsxParsingService;
    private readonly IRetrieveDataService<Achievement> _retrieveDataService = retrieveDataService;
    private readonly IProcessDataService<AchievementCsvModel> _processDataService = processDataService;


    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _retrieveDataService.GetAsync();
        return Ok(members);
    }

    /// <summary>
    /// Uploads a CSV file to update achievement data.
    /// </summary>
    /// <param name="file">The CSV file containing achievement data.</param>
    /// <returns>A success message if the file is processed correctly.</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
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