namespace CAPSquadron_Shared.Services.CadetTracker;

public interface ICadetTrackerService
{
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids);
    Task<PersonalCadetTrackerDto> GetCadetTrackerByCapidAsync(int capids);
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync();
}