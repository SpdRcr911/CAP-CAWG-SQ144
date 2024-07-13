namespace CAPSquadron_WebServer.Services.QualityCadetUnit;

public interface IQualityCadetUnitReportService
{
    Task<IEnumerable<QualityCadetUnitReport>> GetReportsAsync(CancellationToken cancellationToken = default);
}