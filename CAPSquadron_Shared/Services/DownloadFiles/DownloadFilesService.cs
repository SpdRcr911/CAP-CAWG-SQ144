namespace CAPSquadron_Shared.Services.DownloadFiles;

public class DownloadFilesService(IHttpClientFactory httpClientFactory) : IDownloadFilesService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CadetAPI");

    public async Task<byte[]> GetCadetPhysicalFitnessTrainingFlightWorkSheetAsync()
    {
        var response = await _httpClient.GetAsync("api/CadetPhysicalFitnessTrainingReport/download-pt-worksheet");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}
