namespace CAPSquadron_SPA.Services;

public interface ICadetTrackerService
{
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids);
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync();
}