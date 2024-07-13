using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CAPSquadron_API.Services;

public class QualityCadetUnitReportService : IQualityCadetUnitReportService
{
    private readonly AppDbContext _context;

    public QualityCadetUnitReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QualityCadetUnitReport>> GetQCUReports(CancellationToken cancellationToken)
    {
        return await _context.QualityCadetUnitReports
                             .AsNoTracking()
                             .ToListAsync(cancellationToken);
    }

    public async Task<QualityCadetUnitReport> GetQCUReportByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.QualityCadetUnitReports
                              .AsNoTracking()
                              .FirstOrDefaultAsync(r => r.Id == id, cancellationToken) 
                        ?? throw new NotFoundException("QualityCadetUnitReport not found."); ;
    }

}
