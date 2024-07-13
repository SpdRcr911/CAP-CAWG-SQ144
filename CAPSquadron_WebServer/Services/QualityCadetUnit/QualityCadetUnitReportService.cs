
namespace CAPSquadron_WebServer.Services.QualityCadetUnit
{
    public class QualityCadetUnitReportService(ApiClient apiClient) : IQualityCadetUnitReportService
    {
        public async Task<IEnumerable<QualityCadetUnitReport>> GetReportsAsync(CancellationToken cancellationToken = default)
        {
            return await apiClient.GetQCUReportsAsync(cancellationToken);
        }
    }
}
