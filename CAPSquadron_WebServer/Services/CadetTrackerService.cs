﻿namespace CAPSquadron_WebServer.Services;

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
    public async Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync()
    {
        return await _apiClient.GetCadetTrackerAsync();
    }
}