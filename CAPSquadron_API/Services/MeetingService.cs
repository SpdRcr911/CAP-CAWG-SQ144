using CAPSquadron_API.Controllers;
using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public interface IMeetingService
{
    Task<IEnumerable<CallDownResponse>> GetCallDownResponsesAsync(DateOnly meetingDate);
    Task<CallDownResponse> GetCallDownResponseByIdAsync(int id);
    Task<MeetingInfoDto> GetNextMeetingInfoAsync();
    Task<CallDownResponse> RecordCallDown(CallDownResponse response);
}
public class MeetingService(AppDbContext appContext) : IMeetingService
{
    public async Task<IEnumerable<CallDownResponse>> GetCallDownResponsesAsync(DateOnly meetingDate)
    {
        return await appContext.CallDownResponses.Where(c => c.MeetingDate == meetingDate).ToListAsync();
    }
    public async Task<CallDownResponse> GetCallDownResponseByIdAsync(int id)
    {
        return await appContext.CallDownResponses.FirstOrDefaultAsync(c =>c.Id == id) ?? throw new NotFoundException($"Call Down Response not found for id {id}.");
    }

    public async Task<MeetingInfoDto> GetNextMeetingInfoAsync()
    {
        return await Task.FromResult(MeetingDefaults.GetUpcommingMeeting());
    }

    public async Task<CallDownResponse> RecordCallDown(CallDownResponse response)
    {
        var createdCallDown = await appContext.CallDownResponses.AddAsync(response);
        await appContext.SaveChangesAsync();

        return createdCallDown.Entity;
    }
}

public class MeetingDefaults
{
    public static MeetingInfoDto GetUpcommingMeeting()
    {
        var today = DateTime.Today;
        var nextTuesday = GetNextTuesday(today);
        var tuesdayOfTheMonth = GetTuesdayOfMonth(nextTuesday);

        return tuesdayOfTheMonth switch
        {
            1 => new MeetingInfoDto { Title = "1st Tuesday", Date = DateOnly.FromDateTime(nextTuesday), Topic = "Safety", UOD = "Blues" },
            2 => new MeetingInfoDto { Title = "2nd Tuesday", Date = DateOnly.FromDateTime(nextTuesday), Topic = "Aerospace Education", UOD = "ABUs" },
            3 => new MeetingInfoDto { Title = "3rd Tuesday", Date = DateOnly.FromDateTime(nextTuesday), Topic = "PT", UOD = "PT" },
            4 => new MeetingInfoDto { Title = "4th Tuesday", Date = DateOnly.FromDateTime(nextTuesday), Topic = "Character Development", UOD = "ABUs" },
            5 => new MeetingInfoDto { Title = "5th Tuesday", Date = DateOnly.FromDateTime(nextTuesday), Topic = "Fun", UOD = "Civies" },
            _ => new MeetingInfoDto()
        };
    }

    private static DateTime GetNextTuesday(DateTime fromDate)
    {
        int daysUntilTuesday = ((int)DayOfWeek.Tuesday - (int)fromDate.DayOfWeek + 7) % 7;
        return fromDate.AddDays(daysUntilTuesday == 0 ? 7 : daysUntilTuesday);
    }

    private static int GetTuesdayOfMonth(DateTime date)
    {
        // Get the first day of the month
        DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

        // Find the first Tuesday of the month
        DateTime firstTuesday = GetNextTuesday(firstDayOfMonth.AddDays(-1));

        // Calculate the number of weeks between the first Tuesday and the given date
        return (date.Day - firstTuesday.Day) / 7 + 1;
    }

}
