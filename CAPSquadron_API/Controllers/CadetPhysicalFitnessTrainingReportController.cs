using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Tags("Cadet Physical Fitness Training Report")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CadetPhysicalFitnessTrainingReportController(IXlsxParsingService xlsxParsingService, IRetrieveDataService<CadetPhysicalFitnessTrainingReport> retrieveDataService, IProcessDataService<CadetPhysicalFitnessTrainingReportCsv> processDataService, ICadetPhysicalFitnessTrainingReportService cadetPhysicalFitnessService) : ControllerBase
{
    [HttpGet(Name = nameof(GetCadetPhysicalFitnessTrainingReport))]
    [ProducesResponseType<List<CadetPhysicalFitnessTrainingReport>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetPhysicalFitnessTrainingReport()
    {
        var members = await retrieveDataService.GetAsync();
        return Ok(members);
    }

    [HttpGet("download-pt-worksheet",Name = nameof(GetCadetPhysicalFitnessTrainingFlightWorkSheet))]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
    public async Task<IActionResult> GetCadetPhysicalFitnessTrainingFlightWorkSheet()
    {
        byte[] fileContents = await cadetPhysicalFitnessService.GetFitnessWorksheetsAsync();
        string fileName = "FitnessReports.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return File(fileContents, contentType, fileName);
    }

    [HttpPost("upload", Name = nameof(UploadCadetPhysicalFitnessTrainingReportFile))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadCadetPhysicalFitnessTrainingReportFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var cadetPhysicalFitnessTrainingReportCsv = xlsxParsingService.ParseXlsx<CadetPhysicalFitnessTrainingReportCsv>(file.OpenReadStream(), rowOffset: 6, colOffset: 3);
        await processDataService.ProcessAsync(cadetPhysicalFitnessTrainingReportCsv);

        return Ok(new { success = "File successfully uploaded and processed" });
    }
}
