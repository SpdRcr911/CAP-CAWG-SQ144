using CAPSquadron_WebServer.Services.FileHandling;
using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services.Attendance;

public class CadetTrackerFileHandler : IFileHandler
{
    private readonly ApiClient _apiClient;

    public CadetTrackerFileHandler(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public bool CanHandle(IBrowserFile file, string? context)
    {
        return context is not null && context.Equals("achievements", StringComparison.OrdinalIgnoreCase) && 
            file.Name.StartsWith("Cadet_Full_Track_Report", StringComparison.OrdinalIgnoreCase)
               && file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }

    public async Task UploadFileAsync(IBrowserFile file)
    {
        using var stream = file.OpenReadStream();
        var fileParameter = new FileParameter(stream, file.Name, file.ContentType);

        await _apiClient.UploadAchievementFileAsync(fileParameter);
    }
}
