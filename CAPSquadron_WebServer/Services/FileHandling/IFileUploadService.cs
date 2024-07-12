using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services.FileHandling;
public interface IFileUploadService
{
    Task UploadFileAsync(IBrowserFile file, string? context);
}
