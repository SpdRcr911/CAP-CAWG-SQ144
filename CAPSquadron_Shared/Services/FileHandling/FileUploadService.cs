﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace CAPSquadron_Shared.Services.FileHandling;

public class FileUploadService : IFileUploadService
{
    private readonly IFileHandlerFactory _fileHandlerFactory;
    private readonly ILogger<FileUploadService> _logger;

    public FileUploadService(IFileHandlerFactory fileHandlerFactory, ILogger<FileUploadService> logger)
    {
        _fileHandlerFactory = fileHandlerFactory;
        _logger = logger;
    }

    public async Task UploadFileAsync(IBrowserFile file, string? context)
    {
        try
        {
            ValidateFile(file);

            var fileHandler = _fileHandlerFactory.GetFileHandler(file, context);
            await fileHandler.UploadFileAsync(file);
        }
        catch (InvalidDataException ex)
        {
            _logger.LogError(ex, "File validation failed for {FileName}", file.Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while uploading the file {FileName}", file.Name);
            throw new ApplicationException("An error occurred while uploading the file.", ex);
        }
    }

    private void ValidateFile(IBrowserFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file), "File cannot be null.");
        }

        // Add common validation checks if needed
    }
}
