using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface ICadetTrackerService
{
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync();
    Task<PersonalCadetTrackerDto?> GetCadetTrackerByCapidAsync(int capid);
    Task<IEnumerable<PersonalCadetTrackerDto>?> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids);
    Task<IEnumerable<PersonalCadetTrackerDto>?> GetCadetTrackerByAchvNameAsync(string achvName);
}