using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceReportController(IAttendanceReportService attendanceReportService, IProcessDataService<AttendanceReportCsv> processDataService, ICsvParsingService csvParsingService) : ControllerBase
{

    [HttpGet("get-dates", Name = nameof(GetAttendanceReportDates))]
    [ProducesResponseType<IEnumerable<DateTimeOffset?>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceReportDates()
    {
        var attendanceData = await attendanceReportService.GetAttendanceReportDatesAsync();
        return Ok(attendanceData);
    }

    [HttpGet("record/{capid}", Name = nameof(GetAttendanceReportForMember))]
    [ProducesResponseType<IEnumerable<AttendanceReport>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceReportForMember(int capid)
    {
        var attendanceData = await attendanceReportService.GetAttendanceReportForMemberAsync(capid);
        return Ok(attendanceData);
    }

    [HttpGet(Name = nameof(GetAllAttendiesByDate))]
    [ProducesResponseType<IEnumerable<AttendanceReport>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAttendiesByDate(DateTimeOffset date, bool? present)
    {
        var attendanceData = await attendanceReportService.GetAllAttendiesByDateAsync(date, present);
        return Ok(attendanceData);
    }

    [HttpGet("get-capids/{date}",Name = nameof(GetAttendiesCapIdsByDate))]
    [ProducesResponseType<IEnumerable<int>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendiesCapIdsByDate(DateTimeOffset date)
    {
        var attendanceData = await attendanceReportService.GetAttendiesCapIdsByDateAsync(date);
        return Ok(attendanceData);
    }

    [HttpPost("upload", Name = nameof(UploadAttendanceReport))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadAttendanceReport(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        try
        {
            var attendanceReportCsv = csvParsingService.ParseCsv<AttendanceReportCsv>(file.OpenReadStream());
            await processDataService.ProcessAsync(attendanceReportCsv);
            return Ok("Membership list updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }
}
