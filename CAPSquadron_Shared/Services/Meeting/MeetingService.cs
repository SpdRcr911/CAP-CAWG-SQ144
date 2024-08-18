using System.Collections;

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
    public async Task RecordCallDownAsync(CallDownResponse callDownResponse)
    {
        await apiClient.CallDownReesponseAsync(callDownResponse);
    }
    public async Task<IEnumerable<DateOnly>> GetCallDownDatesAsync(DateOnly? meetingDate)
    {
        return (await apiClient.GetCallDownDatesAsync()).ToArray();
    }

    public async Task<IEnumerable<DateOnly>> GetCallDownDatesAsync()
    {
        return await apiClient.GetCallDownDatesAsync();
    }
    public async Task<IEnumerable<CallDownResponse>> GetCallDownsAsync(DateOnly meetingDate)
    {
        return await apiClient.GetCallDownsAsync(meetingDate);

    }
}
