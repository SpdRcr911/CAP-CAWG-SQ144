namespace CAPSquadron_WebServer.Services.Flight;

public class FlightService : IFlightService
{
    private readonly ApiClient _apiClient;

    public FlightService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<FlightDto>> GetFlightsAsync()
    {
        return await _apiClient.GetFlightsAsync();
    }

    public async Task<FlightDto> GetFlightByIdAsync(int id)
    {
        return await _apiClient.GetFlightAsync(id);
    }

    public async Task<IEnumerable<Member>> GetUnassignedCadetsAsync()
    {
        var unassignedCadetCapIds = await _apiClient.GetUnassignedCadetsAsync();
        var unassignedCadets = await _apiClient.GetMembersByCapidsAsync(unassignedCadetCapIds);
        return unassignedCadets;
    }

    public async Task<FlightDto> UpdateFlight(int flightId, FlightDto flightDto)
    {
        return await _apiClient.UpdateFlightAsync(flightId, flightDto);
    }
}
