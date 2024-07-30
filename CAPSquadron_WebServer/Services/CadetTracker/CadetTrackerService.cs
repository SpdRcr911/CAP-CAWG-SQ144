namespace CAPSquadron_WebServer.Services.CadetTracker;

public class CadetTrackerService : ICadetTrackerService
{
    private readonly ApiClient _apiClient;

    public CadetTrackerService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids)
    {
        return await _apiClient.GetCadetTrackerByCapidsAsync(capids);
    }

    public async Task<PersonalCadetTrackerDto> GetCadetTrackerByCapidAsync(int capids)
    {
        return await _apiClient.GetCadetTrackerByCapidAsync(capids);
    }

    public async Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync()
    {
        return await _apiClient.GetCadetTrackerAsync();
    }
}