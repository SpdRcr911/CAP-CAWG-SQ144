using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlights()
    {
        var flights = await _flightService.GetFlightsAsync();
        return Ok(flights);
    }

    [HttpGet("{id}")]
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

    [HttpPost]
    public async Task<IActionResult> CreateFlight(FlightDto flightDto)
    {
        var flight = await _flightService.CreateFlightAsync(flightDto);
        return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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

    [HttpGet("unassigned-or-commanders-or-sergeants")]
    public async Task<IActionResult> GetUnassignedOrCommandersOrSergeants()
    {
        var capIds = await _flightService.GetUnassignedOrCommandersOrSergeantsAsync();
        return Ok(capIds);
    }
}