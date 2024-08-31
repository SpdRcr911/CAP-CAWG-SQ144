using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadetReportsController(ICadetReportsService cadetReportingService) : ControllerBase
{

    // Get summary by phase
    [HttpGet("phases")]
    public async Task<ActionResult<IEnumerable<PhaseSummaryDto>>> GetPhaseSummary()
    {
        var phaseSummary = await cadetReportingService.GetPhaseSummaryAsync();
        return Ok(phaseSummary);
    }

    // Get achievement summary for a specific phase
    [HttpGet("phases/{phase}/achievements")]
    public async Task<ActionResult<IEnumerable<AchievementSummaryDto>>> GetAchievementSummary(string phase)
    {
        var achievementSummary = await cadetReportingService.GetAchievementSummaryAsync(phase);
        if (achievementSummary == null)
        {
            return NotFound();
        }
        return Ok(achievementSummary);
    }

    // Get cadet details for a specific phase and achievement
    [HttpGet("phases/{phase}/achievements/{achievementName}/cadets")]
    public async Task<ActionResult<IEnumerable<CadetDetailsDto>>> GetCadetDetails(string phase, string achievementName)
    {
        var cadetDetails = await cadetReportingService.GetCadetDetailsAsync(phase, achievementName);
        if (cadetDetails == null)
        {
            return NotFound();
        }
        return Ok(cadetDetails);
    }
}
