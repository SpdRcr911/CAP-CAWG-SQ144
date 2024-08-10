namespace CAPSquadron_WebServer.Services.Attendance;

public class AttendanceSignInService(ApiClient apiClient) : IAttendanceSignInService
{
    public async Task<AttendanceSignIn> GetAttendanceSignInAsync(int capid)
    {
        return await apiClient.GetAttendanceSignInAsync(capid);
    }

    public async Task<IEnumerable<AttendanceSignIn>> GetAttendanceSignInsAsync()
    {
        return await apiClient.GetAttendanceSignInsAsync();
    }

    public async Task<ICollection<AttendanceSignIn>> GetAttendanceSignInsAsync(IEnumerable<int> ids)
    {
        return await apiClient.GetAttendanceSignInsByCapidsAsync(ids);
    }
}