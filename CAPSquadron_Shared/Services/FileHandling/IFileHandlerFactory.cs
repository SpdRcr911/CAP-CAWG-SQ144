using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_Shared.Services.FileHandling;

public interface IFileHandlerFactory
{
    IFileHandler GetFileHandler(IBrowserFile file, string? context);
}
