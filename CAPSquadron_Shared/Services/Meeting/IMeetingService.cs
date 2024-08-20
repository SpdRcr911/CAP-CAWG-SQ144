
namespace CAPSquadron_Shared.Services.Meeting;

public interface IMeetingService
{
    IEnumerable<string> GetAvailableRequestsForCadet(PersonalCadetTrackerDto personalCadetTracker);
    Task<IEnumerable<DateOnly>> GetCallDownDatesAsync();
    Task<MeetingInfoDto> GetNextMeeting(DateOnly? meetingDate = null);
    Task RecordCallDownAsync(CallDownResponse callDownResponse);
    Task<IEnumerable<CallDownResponse>> GetCallDownsAsync(DateOnly meetingDate, int? capId = null);
}
