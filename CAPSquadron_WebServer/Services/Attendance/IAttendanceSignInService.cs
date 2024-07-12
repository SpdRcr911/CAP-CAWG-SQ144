namespace CAPSquadron_WebServer.Services.Attendance;

public interface IAttendanceSignInService
{
    Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync();

    Task<IEnumerable<AttendanceSignIn>> GetAttendanceSignInsAsync();
    Task<AttendanceSignIn> GetAttendanceSignInAsync(int capid);
    Task<ICollection<AttendanceSignIn>> GetAttendanceSignInsAsync(IEnumerable<int> capids);

}
