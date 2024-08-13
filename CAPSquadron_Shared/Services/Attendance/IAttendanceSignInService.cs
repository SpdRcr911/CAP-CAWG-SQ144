namespace CAPSquadron_Shared.Services.Attendance;

public interface IAttendanceSignInService
{
    Task<IEnumerable<AttendanceSignIn>> GetAttendanceSignInsAsync();
    Task<AttendanceSignIn> GetAttendanceSignInAsync(int capid);
    Task<ICollection<AttendanceSignIn>> GetAttendanceSignInsAsync(IEnumerable<int> capids);

}
