namespace CAPSquadron_WebServer.Services.Attendance;

public interface IAttendanceService
{
    public Task<ICollection<AttendanceReport>> GetAttendanceReportForMemberAsync(int capId, DateOnly? startDate, bool? present);
    public Task<ICollection<DateOnly>> GetLastSixMonthTuesdays();
    public Task<Dictionary<DateOnly, bool>> GetCadetsAttendanceAsync(int capId, DateOnly? startDate);
}

public class AttendanceService(ApiClient apiClient) : IAttendanceService
{
    public async Task<ICollection<DateOnly>> GetLastSixMonthTuesdays()
    {
        var lastSixMonths = DateTime.Now.AddMonths(-6);

        return await apiClient.GetAttendanceReportDatesAsync(new DateOnly(lastSixMonths.Year, lastSixMonths.Month, lastSixMonths.Day), true);
    }

    public async Task<ICollection<AttendanceReport>> GetAttendanceReportForMemberAsync(int capId, DateOnly? startDate, bool? present)
    {
        return await apiClient.GetAttendanceReportForMemberAsync(capId, startDate, present);
    }

    public async Task<Dictionary<DateOnly, bool>> GetCadetsAttendanceAsync(int capId, DateOnly? startDate)
    {
        var tuesdays = await apiClient.GetAttendanceReportDatesAsync(startDate, true);

        var attendanceDates = (await apiClient.GetAttendanceReportForMemberAsync(capId, startDate, true))
                              .Select(att => new DateOnly(att.StartDate!.Value.Year, att.StartDate!.Value.Month, att.StartDate!.Value.Day));

        var cadetsAttendance = tuesdays.ToDictionary(
            tuesday => tuesday,
            tuesday => attendanceDates.Contains(tuesday)
        );

        return cadetsAttendance;
    }
}
