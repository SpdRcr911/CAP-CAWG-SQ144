namespace CAPSquadron_Shared.Services.Meeting;

public interface IMeetingService
{
    IEnumerable<string> GetAvailableRequestsForCadet(PersonalCadetTrackerDto personalCadetTracker);
    Task<MeetingInfoDto> GetNextMeeting();
    Task RecordCallDownAsync(CallDownResponse callDownResponse);
}
