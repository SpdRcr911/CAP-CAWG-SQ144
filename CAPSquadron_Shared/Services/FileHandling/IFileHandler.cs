using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_Shared.Services.FileHandling;

public interface IFileHandler
{
    Task UploadFileAsync(IBrowserFile file);
    bool CanHandle(IBrowserFile file, string? context);
}
