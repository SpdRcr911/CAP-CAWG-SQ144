using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services.FileHandling;

public interface IFileHandler
{
    Task UploadFileAsync(IBrowserFile file);
    bool CanHandle(IBrowserFile file, string? context);
}
