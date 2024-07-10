using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    /// <summary>
    /// Gets all flights
    /// </summary>
    /// <returns>List of flights</returns>
    [HttpGet(Name = nameof(GetFlights))]
    [ProducesResponseType<IEnumerable<FlightDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFlights()
    {
        var flights = await _flightService.GetFlightsAsync();
        return Ok(flights);
    }

    /// <summary>
    /// Get a flight by flight Id
    /// </summary>
    /// <param name="id">flight id</param>
    /// <returns>A single flight</returns>
    [HttpGet("{id}", Name = nameof(GetFlight))]
    [ProducesResponseType<FlightDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFlight(int id)
    {
        try
        {
            var flight = await _flightService.GetFlightAsync(id);
            return Ok(flight);
        }
        catch (NotFoundException nfex)
        {
            return NotFound(new { message = nfex.Message });
        }
    }

    [HttpPost(Name = nameof(CreateFlight))]
    [ProducesResponseType<FlightDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateFlight(FlightDto flightDto)
    {
        var flight = await _flightService.CreateFlightAsync(flightDto);
        return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
    }

    [HttpPut("{id}", Name = nameof(UpdateFlight))]
    [ProducesResponseType<FlightDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateFlight(int id, FlightDto flightDto)
    {
        try
        {
            var updatedFlight = await _flightService.UpdateFlightAsync(id, flightDto);
            return Ok(updatedFlight);
        }
        catch (NotFoundException nfex)
        {
            return NotFound(new { message = nfex.Message });
        }
    }

    [HttpDelete("{id}", Name = nameof(DeleteFlight))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        try
        {
            await _flightService.DeleteFlightAsync(id);
            return NoContent();
        }
        catch (NotFoundException nfex)
        {
            return NotFound(new { message = nfex.Message });
        }
    }

    [HttpGet("unassigned-or-commanders-or-sergeants", Name = nameof(GetUnassignedOrCommandersOrSergeants))]
    [ProducesResponseType<IEnumerable<int>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnassignedOrCommandersOrSergeants()
    {
        var capIds = await _flightService.GetUnassignedOrCommandersOrSergeantsAsync();
        return Ok(capIds);
    }

    [HttpGet("{id}/details")]
    public async Task<ActionResult<FlightDetailDto>> GetFlightDetails(int id)
    {
        var flightDetails = await _flightService.GetFlightDetailAsync(id);
        return Ok(flightDetails);
    }
}