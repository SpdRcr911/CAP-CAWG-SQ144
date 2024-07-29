using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CAPSquadron_WebServer.Components;

public partial class FileUpload
{
    private IBrowserFile? selectedFile;
    private string? validationMessage;
    private string? uploadMessage;
    private bool uploadSuccess;
    private bool isUploadVisible = false;
    [Parameter]
    public string? CurrentContext { get; set; } = string.Empty;

    private void ShowUpload()
    {
        isUploadVisible = true;
    }

    private void HideUpload()
    {
        isUploadVisible = false;
        validationMessage = null;
        uploadMessage = null;
        selectedFile = null;
        StateHasChanged();
    }

    private void HandleFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var validationResult = ValidateFile(file);

        if (!validationResult.IsValid)
        {
            validationMessage = validationResult.ErrorMessage;
            selectedFile = null;
            return;
        }

        validationMessage = null;
        selectedFile = file;
    }

    private async Task SubmitFile()
    {
        if (selectedFile is not null)
        {
            try
            {
                await FileUploadService.UploadFileAsync(selectedFile, CurrentContext);
                uploadMessage = "File uploaded successfully!";
                uploadSuccess = true;
                _ = DelayAndHideUpload();
            }
            catch (Exception ex)
            {
                uploadMessage = $"File upload failed: {ex.Message}";
                uploadSuccess = false;
            }
            finally
            {
                selectedFile = null; // Clear the file after upload
            }
        }
    }
    private async Task DelayAndHideUpload()
    {
        await Task.Delay(5000);
        HideUpload();
    }
    private ValidationResult ValidateFile(IBrowserFile file)
    {
        // Example validation: check file size and extension
        var maxSizeInBytes = 10 * 1024 * 1024; // 10 MB
        var allowedExtensions = new[] { ".csv", ".xlsx" };

        if (file.Size > maxSizeInBytes)
        {
            return new ValidationResult("File size exceeds the maximum allowed size.");
        }

        var fileExtension = Path.GetExtension(file.Name);
        if (!allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
        {
            return new ValidationResult("Invalid file type.");
        }

        return ValidationResult.Success;
    }

    public class ValidationResult
    {
        public bool IsValid => ErrorMessage is null;
        public string? ErrorMessage { get; }

        public ValidationResult(string? errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success => new ValidationResult();
    }
}
