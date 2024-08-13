using CAPSquadron_Shared.Services.FileHandling;
using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_Shared.Services.Attendance;

public class CadetTrackerFileHandler(ApiClient apiClient) : IFileHandler
{
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

        await apiClient.UploadCadetPromotionsFullTrackFileAsync(fileParameter);
    }
}
