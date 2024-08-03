using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public interface IAttendanceReportService
{
    public Task<IEnumerable<DateOnly>> GetAttendanceReportDatesAsync(DateOnly? startDate, bool? tuesdaysOnly);
    public Task<IEnumerable<int>> GetAttendiesCapIdsByDateAsync(DateOnly date);
    public Task<IEnumerable<AttendanceReport>> GetAllAttendiesByDateAsync(DateOnly date, bool? present);
    public Task<IEnumerable<AttendanceReport>> GetAttendanceReportForMemberAsync(int CAPID, DateOnly? startDate, bool? present);
}

public class AttendanceReportService(AppDbContext context) : IAttendanceReportService, IProcessDataService<AttendanceReportCsv>
{
    public async Task<IEnumerable<DateOnly>> GetAttendanceReportDatesAsync(DateOnly? startDate, bool? tuesdaysOnly)
    {
        var dates = context.AttendanceReports.AsQueryable();
        if (startDate.HasValue)
            dates = dates.Where(ar => DateTime.Compare(ar.StartDate!.Value.Date, new DateTime(startDate.Value, default)) == 1);

        if (tuesdaysOnly.HasValue)
            dates = dates.Where(ar => ar.StartDate!.Value.DayOfWeek == DayOfWeek.Tuesday);

        return await dates.Select(d=> new DateOnly(d.StartDate!.Value.Year, d.StartDate!.Value.Month, d.StartDate!.Value.Day)).Distinct().OrderByDescending(d=>d).ToListAsync();
    }

    public async Task<IEnumerable<int>> GetAttendiesCapIdsByDateAsync(DateOnly date)
    {
        return await context.AttendanceReports.Where(d => DateTime.Compare(d.StartDate!.Value.Date,date.ToDateTime(default)) == 0 && d.IsPresent == true).Select(c => c.CAPID).ToListAsync();
    }

    public async Task<IEnumerable<AttendanceReport>> GetAllAttendiesByDateAsync(DateOnly date, bool? present = null)
    {
        var query = context.AttendanceReports.Where(d => DateTime.Compare(d.StartDate!.Value.Date, date.ToDateTime(default)) == 0 );

        if (present is not null)
            query = query.Where(d => d.IsPresent == present);

        return await query.ToListAsync();
    }

    public async Task ProcessAsync(IEnumerable<AttendanceReportCsv> attendanceReportCsvs)
    {
        context.TruncateAttendanceReports();

        foreach (var attendance in attendanceReportCsvs)
        {
            var newAttendance = new AttendanceReport()
            {
                StartDate = attendance.StartDate?.ToLocalTime().ToUniversalTime(),
                Topics = attendance.Topics,
                IsDrillAndCeremony = attendance.TxtDrillAndCeremony == "X",
                IsAET = attendance.TxtAET == "X",
                IsPhysicalFitnessTesting = attendance.Topic_PhysicalFitnessTesting == "X",
                IsSBT = attendance.TxtSBT == "X",
                IsCadetPrograms = attendance.TextCadetPrograms == "X",
                IsCharacterDevelopment = attendance.TextCharacterDevelopment == "X",
                IsCommunityService = attendance.TextCommunityService == "X",
                Location = attendance.Location,
                IsOther = attendance.TxtOther == "X",
                Other = attendance.TextOther,
                Section = attendance.SECTION,
                Name = attendance.Name,
                Address = attendance.TxtAddress,
                Phone = attendance.TxtPhone,
                Website = attendance.TxtWebsite,
                MeetingInfo = attendance.TxtMeetingInfo,
                FullName = attendance.FullName,
                Rank = attendance.Rank,
                CAPID = attendance.CAPID ?? 0, // Handle null value
                Expiration = attendance.Expiration,
                IsPresent = attendance.Present == "X",
                IsExcused = attendance.Excused == "X",
                HasUniform = attendance.Uniform == "X",
                HasCAPF160_161 = attendance.TxtCAPF160_161 == "X",
                EOCurrent = attendance.TxtEO == "X",
                OPSECCurrent = attendance.TxtOPSEC == "X",
                SafetyCurrent = attendance.Safety == "X",
                GuestName = attendance.GstName,
                GuestRank = attendance.GstRank,
                GuestPhone = attendance.GstPhone,
                GuestEmail = attendance.GstEmail,
                GuestNotes = attendance.GstNotes,
                OverallNotes = attendance.Textbox3
            };

            context.AttendanceReports.Add(newAttendance);
        }

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AttendanceReport>> GetAttendanceReportForMemberAsync(int CAPID, DateOnly? startDate, bool? present)
    {
        var attendanceReports = context.AttendanceReports.AsQueryable();
        if (startDate.HasValue)
            attendanceReports = attendanceReports.Where(ar => DateTime.Compare(ar.StartDate!.Value.Date, startDate.Value.ToDateTime(default)) == 1);

        if (present.HasValue)
            attendanceReports = attendanceReports.Where(ar => ar.IsPresent == present);

        return await attendanceReports.Where(a=> a.CAPID == CAPID).ToListAsync();
    }
}
