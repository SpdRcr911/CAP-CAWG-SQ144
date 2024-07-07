using System.Net.Http.Json;

namespace CAPSquadron_SPA.Services;

public class MemberService : IMemberService
{
    private readonly HttpClient _httpClient;

    public MemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<int>>("/api/Flight/unassigned-or-commanders-or-sergeants") ?? [];
    }
}