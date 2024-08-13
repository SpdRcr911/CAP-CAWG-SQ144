using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class MeetingController(IMeetingService meetingService) : ControllerBase
{
    /// <summary>
    /// Gets next meeting information
    /// </summary>
    /// <returns>Meeting informtaion</returns>
    [HttpGet("next-meeting", Name = nameof(NextMeetingInfo))]
    [ProducesResponseType<MeetingInfoDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> NextMeetingInfo()
    {
        //var flights = await _Service.GetFlightsAsync();

        var meeting = await meetingService.GetNextMeetingInfoAsync();
        return Ok(meeting);
    }

    /// <summary>
    /// Record Call Downs
    /// </summary>
    /// <param name="callDownResponse"></param>
    /// <returns>no response, just 201</returns>
    [HttpPost("call-down-response", Name = nameof(CallDownReesponse))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CallDownReesponse(CallDownResponse callDownResponse)
    {
        await meetingService.RecordCallDown(callDownResponse);
        return Created();
    }

    /// <summary>
    /// Get call downs by date
    /// </summary>
    /// <param name="meetingDate">Date only in the formate of YYYY-MM-DD</param>
    /// <returns></returns>
    [HttpGet("call-downs", Name = nameof(GetCallDowns))]
    [ProducesResponseType<CallDownResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCallDowns(DateOnly meetingDate)
    {
        var callDownResponses = await meetingService.GetCallDownResponses(meetingDate);
        return Ok(callDownResponses);
    }
}
