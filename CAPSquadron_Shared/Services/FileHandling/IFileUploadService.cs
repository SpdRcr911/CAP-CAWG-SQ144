using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_Shared.Services.FileHandling;
public interface IFileUploadService
{
    Task UploadFileAsync(IBrowserFile file, string? context);
}
