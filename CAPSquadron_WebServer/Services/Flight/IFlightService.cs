namespace CAPSquadron_WebServer.Services.Flight;

public interface IFlightService
{
    Task<IEnumerable<FlightDto>> GetFlightsAsync();
    Task<FlightDto> GetFlightByIdAsync(int id);
}
