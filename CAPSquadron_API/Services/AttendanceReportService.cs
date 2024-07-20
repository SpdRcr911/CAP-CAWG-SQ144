﻿using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public interface IAttendanceReportService
{
    public Task<IEnumerable<DateTimeOffset?>> GetAttendanceReportDatesAsync();
    public Task<IEnumerable<int>> GetAttendiesCapIdsByDateAsync(DateTimeOffset? date);
    public Task<IEnumerable<AttendanceReport>> GetAllAttendiesByDateAsync(DateTimeOffset? date, bool? present);
    public Task<IEnumerable<AttendanceReport>> GetAttendanceReportForMemberAsync(int CAPID);
}

public class AttendanceReportService(AppDbContext context) : IAttendanceReportService, IProcessDataService<AttendanceReportCsv>
{
    public async Task<IEnumerable<DateTimeOffset?>> GetAttendanceReportDatesAsync()
    {
        return await context.AttendanceReports.Select(d=> d.StartDate).Distinct().OrderByDescending(d=>d).ToListAsync();
    }

    public async Task<IEnumerable<int>> GetAttendiesCapIdsByDateAsync(DateTimeOffset? date)
    {
        return await context.AttendanceReports.Where(d => d.StartDate == date && d.IsPresent == true).Select(c => c.CAPID).ToListAsync();
    }

    public async Task<IEnumerable<AttendanceReport>> GetAllAttendiesByDateAsync(DateTimeOffset? date, bool? present = null)
    {
        var query = context.AttendanceReports.Where(d => d.StartDate == date);

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

    public async Task<IEnumerable<AttendanceReport>> GetAttendanceReportForMemberAsync(int CAPID)
    {
        return await context.AttendanceReports.Where(a=> a.CAPID == CAPID).ToListAsync();
    }
}
