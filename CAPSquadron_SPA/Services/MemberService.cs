using System.Net.Http.Json;

namespace CAPSquadron_SPA.Services;

public class MemberService : IMemberService
{
    private readonly ApiClient _apiClient;

    public MemberService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync()
    {
        return await _apiClient.GetUnassignedOrCommandersOrSergeantsAsync();
    }
}