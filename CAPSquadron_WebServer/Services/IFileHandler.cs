using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services;

public interface IFileHandler
{
    Task UploadFileAsync(IBrowserFile file);
    bool CanHandle(IBrowserFile file);
}
