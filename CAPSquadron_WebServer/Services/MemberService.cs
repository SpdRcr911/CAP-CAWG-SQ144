namespace CAPSquadron_WebServer.Services;

public class MemberService(ApiClient apiClient) : IMemberService
{
    public async Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync()
    {
        return await apiClient.GetUnassignedOrCommandersOrSergeantsAsync();
    }

    public async Task<Member> GetMemberAsync(int capid)
    {
        return await apiClient.GetMemberAsync(capid);
    }

    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        return await apiClient.GetMembersAsync();
    }

    public async Task<ICollection<Member>> GetMembersAsync(IEnumerable<int> ids)
    {
        return await apiClient.GetMembersByCapidsAsync(ids);
    }
}