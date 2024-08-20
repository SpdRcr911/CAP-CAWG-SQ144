using CAPSquadron_API.Controllers;
using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public interface IMeetingService
{
    Task<IEnumerable<CallDownResponse>> GetCallDownResponsesAsync(DateOnly meetingDate, int? capId);
    Task<CallDownResponse> GetCallDownResponseByIdAsync(int id);
    Task<MeetingInfoDto> GetNextMeetingInfoAsync(DateOnly? meetingDate);
    Task<CallDownResponse> RecordCallDown(CallDownResponse response);
    Task<IEnumerable<DateOnly>> GetCallDownDatesAsync();
}
public class MeetingService(AppDbContext appContext) : IMeetingService
{
    public async Task<IEnumerable<CallDownResponse>> GetCallDownResponsesAsync(DateOnly meetingDate, int? capId)
    {
        var query = appContext.CallDownResponses.Where(c => c.MeetingDate == meetingDate);
        if (capId.HasValue)
        {
            query = query.Where(c=> c.CapId == capId);
        }

        return await query.ToListAsync();
    }
    public async Task<CallDownResponse> GetCallDownResponseByIdAsync(int id)
    {
        return await appContext.CallDownResponses.FirstOrDefaultAsync(c =>c.Id == id) ?? throw new NotFoundException($"Call Down Response not found for id {id}.");
    }

    public async Task<MeetingInfoDto> GetNextMeetingInfoAsync(DateOnly? meetingDate = null)
    {
        return await Task.FromResult(MeetingDefaults.GetUpcommingMeeting(meetingDate?.ToDateTime(new TimeOnly())));
    }

    public async Task<CallDownResponse> RecordCallDown(CallDownResponse response)
    {
        var current = await appContext.CallDownResponses.FirstOrDefaultAsync(cd => cd.CapId == response.CapId && cd.MeetingDate == response.MeetingDate);
        if (current != null)
        {
            current.Attending = response.Attending;
            current.Requests = response.Requests;
            current.Comments = response.Comments;
            current.Reason = response.Reason;

            await appContext.SaveChangesAsync();

            return current;
        }
        else
        {
            var createdCallDown = await appContext.CallDownResponses.AddAsync(response);
            await appContext.SaveChangesAsync();

            return createdCallDown.Entity;
        }
    }

    public async Task<IEnumerable<DateOnly>> GetCallDownDatesAsync()
    {
        var calldowndates = await appContext.CallDownResponses.Select(d=> d.MeetingDate).Distinct().OrderByDescending(d=>d).ToListAsync();
        return calldowndates;
    }
}

public class MeetingDefaults
{
    public static MeetingInfoDto GetUpcommingMeeting(DateTime? meetinDate = null)
    {
        var today = meetinDate ?? DateTime.Today;
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
        return fromDate.AddDays(daysUntilTuesday);
    }

    private static int GetTuesdayOfMonth(DateTime date)
    {
        // Get the first day of the month
        DateTime firstDayOfMonth = new(date.Year, date.Month, 1);

        // Find the first Tuesday of the month
        DateTime firstTuesday = GetNextTuesday(firstDayOfMonth.AddDays(-1));

        // Calculate the number of weeks between the first Tuesday and the given date
        return (date.Day - firstTuesday.Day) / 7 + 1;
    }

}
