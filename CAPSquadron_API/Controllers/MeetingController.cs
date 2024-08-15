using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    [HttpPost("call-down/response", Name = nameof(CallDownReesponse))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CallDownReesponse(CallDownResponse callDownResponse)
    {
        var createdCallDown = await meetingService.RecordCallDown(callDownResponse);

        return CreatedAtAction(
            actionName: nameof(GetCallDownById),  
            routeValues: new { id = createdCallDown.Id },  
            value: createdCallDown 
        );
    }

    /// <summary>
    /// Get call downs by date
    /// </summary>
    /// <param name="meetingDate">Date only in the formate of YYYY-MM-DD</param>
    /// <returns></returns>
    [HttpGet("call-down", Name = nameof(GetCallDowns))]
    [ProducesResponseType<CallDownResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCallDowns(DateOnly meetingDate)
    {
        var callDownResponses = await meetingService.GetCallDownResponsesAsync(meetingDate);
        if (callDownResponses is null || !callDownResponses.Any())
        {
            return NotFound(new ProblemDetails() { Detail = $"Call downs not found for date {meetingDate}." });
        }
        return Ok(callDownResponses);
    }

    /// <summary>
    /// Get call downs by date
    /// </summary>
    /// <param name="id">ID of the call down response</param>
    /// <returns></returns>
    [HttpGet("call-down/{id:int}", Name = nameof(GetCallDownById))]
    [ProducesResponseType<CallDownResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCallDownById(int id)
    {
        try
        {
            var callDownResponse = await meetingService.GetCallDownResponseByIdAsync(id);
            if (callDownResponse is null)
            {
                return NotFound(new ProblemDetails() { Detail = $"Call Down Response not found for id {id}." });
            }
            return Ok(callDownResponse);
        }
        catch (NotFoundException nfex)
        {
            return NotFound(new ProblemDetails() { Detail = nfex.Message });

        }
    }
}
