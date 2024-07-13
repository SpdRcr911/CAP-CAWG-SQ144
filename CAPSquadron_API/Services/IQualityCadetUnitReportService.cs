using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface IQualityCadetUnitReportService
{
    Task<IEnumerable<QualityCadetUnitReport>> GetQCUReports(CancellationToken cancellationToken);
    Task<QualityCadetUnitReport> GetQCUReportByIdAsync(int id, CancellationToken cancellationToken);
}
