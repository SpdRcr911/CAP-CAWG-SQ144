using CAPSquadron_Shared.Services.Attendance;
using Microsoft.AspNetCore.Components;

namespace CAPSquadron_WebServer.Components
{
    public partial class CadetAttendanceReport
    {
        [Parameter]
        public DateOnly? JoinDate { get; set; }

        [Parameter]
        public int? CapId { get; set; }

        protected IEnumerable<IGrouping<(int Year, int Month), KeyValuePair<DateOnly, bool>>>? GroupedByMonth;

        protected override async Task OnParametersSetAsync()
        {
            if (CapId.HasValue && JoinDate.HasValue)
            {
                GroupedByMonth = await GetCadetAttendanceAsync(CapId.Value, JoinDate.Value);
            }
        }

        private double OverallPercentage
        {
            get
            {
                if (GroupedByMonth == null || !GroupedByMonth.Any())
                {
                    return 0;
                }

                // Calculate total days and attended days
                int totalDays = GroupedByMonth.Sum(group => group.Count());
                int attendedDays = GroupedByMonth.Sum(group => group.Count(report => report.Value));

                return totalDays > 0 ? (double)attendedDays / totalDays * 100 : 0;
            }
        }
        private async Task<IEnumerable<IGrouping<(int Year, int Month), KeyValuePair<DateOnly, bool>>>> GetCadetAttendanceAsync(int capId, DateOnly joinDate)
        {
            var attendanceReports = await attendanceService.GetCadetsAttendanceAsync(capId, joinDate);

            return attendanceReports.GroupBy(report => (report.Key.Year, report.Key.Month))
                                    .OrderByDescending(group => group.Key.Year)
                                    .ThenByDescending(group => group.Key.Month);
        }
    }
}
