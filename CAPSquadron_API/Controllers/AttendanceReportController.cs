using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceReportController(IAttendanceReportService attendanceReportService, IProcessDataService<AttendanceReportCsv> processDataService, ICsvParsingService csvParsingService) : ControllerBase
{

    [HttpGet("get-dates", Name = nameof(GetAttendanceReportDates))]
    [ProducesResponseType<IEnumerable<DateOnly>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceReportDates(DateOnly? startDate, bool? tuesdaysOnly)
    {
        var attendanceData = await attendanceReportService.GetAttendanceReportDatesAsync(startDate, tuesdaysOnly);
        return Ok(attendanceData);
    }

    [HttpGet("record/{capid}", Name = nameof(GetAttendanceReportForMember))]
    [ProducesResponseType<IEnumerable<AttendanceReport>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceReportForMember(int capid, DateOnly? startDate, bool? present)
    {
        var attendanceData = await attendanceReportService.GetAttendanceReportForMemberAsync(capid, startDate, present);
        return Ok(attendanceData);
    }

    [HttpGet(Name = nameof(GetAllAttendiesByDate))]
    [ProducesResponseType<IEnumerable<AttendanceReport>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAttendiesByDate(DateOnly date, bool? present)
    {
        var attendanceData = await attendanceReportService.GetAllAttendiesByDateAsync(date, present);
        return Ok(attendanceData);
    }

    [HttpGet("get-capids/{date}", Name = nameof(GetAttendiesCapIdsByDate))]
    [ProducesResponseType<IEnumerable<int>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendiesCapIdsByDate(DateOnly date)
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
