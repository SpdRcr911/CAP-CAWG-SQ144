namespace CAPSquadron_WebServer.Services;

public interface ICadetTrackerService
{
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids);
    Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync();
}