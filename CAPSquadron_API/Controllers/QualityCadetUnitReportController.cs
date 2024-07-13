using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QualityCadetUnitReportController : ControllerBase
{
    private readonly IQualityCadetUnitReportService _service;

    public QualityCadetUnitReportController(IQualityCadetUnitReportService service)
    {
        _service = service;
    }

    [HttpGet(Name = nameof(GetQCUReports))]
    [ProducesResponseType<IEnumerable<QualityCadetUnitReport>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<QualityCadetUnitReport>>> GetQCUReports(CancellationToken cancellationToken)
    {
        var reports = await _service.GetQCUReports(cancellationToken);
        return Ok(reports);
    }

    [HttpGet("{id}", Name = nameof(GetQCUReportById))]
    [ProducesResponseType<QualityCadetUnitReport>(StatusCodes.Status200OK)]
    public async Task<ActionResult<QualityCadetUnitReport>> GetQCUReportById(int id, CancellationToken cancellationToken)
    {
        var report = await _service.GetQCUReportByIdAsync(id, cancellationToken);
        if (report == null)
        {
            return NotFound();
        }
        return Ok(report);
    }
}
