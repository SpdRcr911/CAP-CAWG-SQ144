using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_Shared.Services.FileHandling;

public class FileHandlerFactory : IFileHandlerFactory
{
    private readonly IEnumerable<IFileHandler> _fileHandlers;

    public FileHandlerFactory(IEnumerable<IFileHandler> fileHandlers)
    {
        _fileHandlers = fileHandlers;
    }

    public IFileHandler GetFileHandler(IBrowserFile file, string? context)
    {
        return _fileHandlers.FirstOrDefault(handler => handler.CanHandle(file, context))
               ?? throw new InvalidOperationException("No handler found for the specified file.");
    }
}
