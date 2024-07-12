using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Services;

public interface IFileHandlerFactory
{
    IFileHandler GetFileHandler(IBrowserFile file);
}
