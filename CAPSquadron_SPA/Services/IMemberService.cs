namespace CAPSquadron_SPA.Services;

public interface IMemberService
{
    Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync();
}
