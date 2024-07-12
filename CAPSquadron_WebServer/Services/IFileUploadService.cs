using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services;
public interface IFileUploadService
{
    Task UploadFileAsync(IBrowserFile file);
}
