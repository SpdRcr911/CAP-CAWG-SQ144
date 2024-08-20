namespace CAPSquadron_Shared.Services.DownloadFiles
{
    public interface IDownloadFilesService
    {
        Task<byte[]> GetCadetPhysicalFitnessTrainingFlightWorkSheetAsync();
    }
}
