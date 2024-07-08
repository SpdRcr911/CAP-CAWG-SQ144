using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CadetTrackerController : ControllerBase
{
    private readonly ICadetTrackerService _cadetTrackerService;

    public CadetTrackerController(ICadetTrackerService cadetTrackerService)
    {
        _cadetTrackerService = cadetTrackerService;
    }

    [HttpGet(Name = nameof(GetCadetTracker))]
    [ProducesResponseType<IEnumerable<PersonalCadetTrackerDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetTracker()
    {
        var cadetTrackerData = await _cadetTrackerService.GetCadetTrackerAsync();
        return Ok(cadetTrackerData);
    }
    [HttpGet("capid/{capid}", Name = nameof(GetCadetTrackerByCapid))]
    [ProducesResponseType<PersonalCadetTrackerDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetTrackerByCapid(int capid)
    {
        var cadetTrackerData = await _cadetTrackerService.GetCadetTrackerByCapidAsync(capid);
        return Ok(cadetTrackerData);
    }

    [HttpPost("capids", Name = nameof(GetCadetTrackerByCapids))]
    [ProducesResponseType<IEnumerable<PersonalCadetTrackerDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetTrackerByCapids([FromBody] IEnumerable<int> capids)
    {
        var cadetTrackerData = await _cadetTrackerService.GetCadetTrackerByCapidsAsync(capids);
        return Ok(cadetTrackerData);
    }

    [HttpGet("achievement/{achvName}", Name = nameof(GetCadetTrackerByAchvName))]
    [ProducesResponseType<IEnumerable<PersonalCadetTrackerDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetTrackerByAchvName(string achvName)
    {
        var cadetTrackerData = await _cadetTrackerService.GetCadetTrackerByAchvNameAsync(achvName);
        return Ok(cadetTrackerData);
    }
}
