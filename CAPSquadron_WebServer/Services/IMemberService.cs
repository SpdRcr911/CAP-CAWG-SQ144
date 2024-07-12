namespace CAPSquadron_WebServer.Services;

public interface IMemberService
{
    Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync();

    Task<IEnumerable<Member>> GetMembersAsync();
    Task<Member> GetMemberAsync(int capid);
    Task<ICollection<Member>> GetMembersAsync(IEnumerable<int> capids);

}
