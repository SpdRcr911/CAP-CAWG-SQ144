namespace CAPSquadron_WebServer.Services.CadetTracker;

public interface ICadetTrackerService
{
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids);
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync();
}