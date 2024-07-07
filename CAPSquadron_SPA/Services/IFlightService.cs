namespace CAPSquadron_SPA.Services;

public interface IFlightService
{
    Task<IEnumerable<FlightDto>> GetFlightsAsync();
    Task<FlightDto> GetFlightByIdAsync(int id);
}
