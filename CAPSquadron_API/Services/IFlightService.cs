using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface IFlightService
{
    Task<IEnumerable<FlightDto>> GetFlightsAsync();
    Task<FlightDto> GetFlightAsync(int id);
    Task<FlightDto> CreateFlightAsync(FlightDto flightDto);
    Task<FlightDto> UpdateFlightAsync(int id, FlightDto flightDto);
    Task DeleteFlightAsync(int id);
    Task<IEnumerable<int>> GetUnassignedCadetsAsync();
    Task<FlightDetailDto> GetFlightDetailAsync(int id);
}