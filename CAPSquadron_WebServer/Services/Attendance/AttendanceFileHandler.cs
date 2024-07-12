using CAPSquadron_WebServer.Services.FileHandling;
using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services.Attendance;

public class AttendanceFileHandler : IFileHandler
{
    private readonly ApiClient _apiClient;

    public AttendanceFileHandler(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public bool CanHandle(IBrowserFile file, string? context)
    {
        return context is not null && context.Equals("attendees", StringComparison.OrdinalIgnoreCase) &&
               file.Name.StartsWith("Attendance_Sign-In_Sheet_(Blank)", StringComparison.OrdinalIgnoreCase) &&
               file.ContentType == "text/csv";
    }

    public async Task UploadFileAsync(IBrowserFile file)
    {
        using var stream = file.OpenReadStream();
        var fileParameter = new FileParameter(stream, file.Name, file.ContentType);

        await _apiClient.UploadAttendanceSignInFileAsync(fileParameter);
    }
}
