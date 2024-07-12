using Microsoft.AspNetCore.Mvc;
using CAPSquadron_API.Services;
using CAPSquadron_API.Models;

namespace CAPSquadron_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AttendanceSignInController(ICsvParsingService csvParsingService, IRetrieveDataService<AttendanceSignIn> retrieveDataService, IProcessDataService<AttendanceSignInCsvModel> processDataService) : ControllerBase
{
    private readonly ICsvParsingService _csvParsingService = csvParsingService;
    private readonly IRetrieveDataService<AttendanceSignIn> _retrieveDataService = retrieveDataService;
    private readonly IProcessDataService<AttendanceSignInCsvModel> _processDataService = processDataService;

    /// <summary>
    /// Gets the list of all attendance sign data.
    /// </summary>
    /// <returns>A list of attendance sign data.</returns>
    [HttpGet(Name = nameof(GetAttendanceSignIns))]
    [ProducesResponseType<List<AttendanceSignIn>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceSignIns()
    {
        var attendanceSignIn = await _retrieveDataService.GetAsync();
        return Ok(attendanceSignIn);
    }

    /// <summary>
    /// Gets attendance sign data by CAPID.
    /// </summary>
    /// <returns>Attendance sign data.</returns>
    [HttpGet("{capid}", Name = nameof(GetAttendanceSignIn))]
    [ProducesResponseType<AttendanceSignIn>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceSignIn(int capid)
    {
        var attendanceSignIn = await _retrieveDataService.GetAsync(capid);
        return Ok(attendanceSignIn);
    }

    /// <summary>
    /// Gets a list of attendance sign data from a list of CAPIDs.
    /// </summary>
    /// <returns>A list of attendance sign data.</returns>
    [HttpPost("capids", Name = nameof(GetAttendanceSignInsByCapids))]
    [ProducesResponseType<IEnumerable<AttendanceSignIn>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttendanceSignInsByCapids([FromBody] IEnumerable<int> capids)
    {
        var cadetTrackerData = await _retrieveDataService.GetAsync(capids);
        return Ok(cadetTrackerData);
    }

    /// <summary>
    /// Uploads a CSV file to update attendance sign data.
    /// </summary>
    /// <remarks>
    /// eService report found in 'Personnel' > 'Attendance Log' > 'Attendance Sign-Sheet (Blank)' > SELECT: Unit, AttendanceSignIn Type ALL, Report Format CSV
    /// </remarks>
    /// <param name="file">The CSV file containing attendance sign data.</param>
    /// <returns>A success message if the file is processed correctly.</returns>
    [HttpPost("upload",Name = nameof(UploadAttendanceSignInFile))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadAttendanceSignInFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var attendanceSignInCsv = _csvParsingService.ParseCsv<AttendanceSignInCsvModel>(file.OpenReadStream());
        await _processDataService.ProcessAsync(attendanceSignInCsv);

        return Ok(new { success = "File successfully uploaded and processed" });
    }
}