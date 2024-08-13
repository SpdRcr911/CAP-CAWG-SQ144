namespace CAPSquadron_Shared.Services.QualityCadetUnit;

public interface IQualityCadetUnitReportService
{
    Task<IEnumerable<QualityCadetUnitReport>> GetReportsAsync(CancellationToken cancellationToken = default);
}