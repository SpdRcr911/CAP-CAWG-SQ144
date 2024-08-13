namespace CAPSquadron_Shared.Services.Meeting;

public class MeetingService (ApiClient apiClient) : IMeetingService
{
    public async Task<MeetingInfoDto> GetNextMeeting()
    {
        return await apiClient.NextMeetingInfoAsync();
    }
    public IEnumerable<string> GetAvailableRequestsForCadet(PersonalCadetTrackerDto personalCadetTracker)
    {
        return ["Drill Test", "Review Board", "Milestone Test", "SDA", "Presentation"];

    }
}
