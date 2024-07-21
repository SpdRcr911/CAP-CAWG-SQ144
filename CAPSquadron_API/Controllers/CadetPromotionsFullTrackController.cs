using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[Tags("Cadetp Promotions Full Track")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CadetPromotionsFullTrackController(IXlsxParsingService xlsxParsingService, IRetrieveDataService<CadetPromotionsFullTrack> retrieveDataService, IProcessDataService<CadetPromotionsFullTrackCsv> processDataService) : ControllerBase
{

    /// <include file='CAPSquadron_API.xml' path='docs/cadetPromotionsFullTrack/get/*'/>
    [HttpGet(Name = nameof(GetCadetPromotionsFullTrack))]
    [ProducesResponseType<List<CadetPromotionsFullTrack>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCadetPromotionsFullTrack()
    {
        var members = await retrieveDataService.GetAsync();
        return Ok(members);
    }

    /// <include file='CAPSquadron_API.xml' path='docs/cadetPromotionsFullTrack/uploadFile/*'/>
    [HttpPost("upload", Name = nameof(UploadCadetPromotionsFullTrackFile))]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadCadetPromotionsFullTrackFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var cadetPromotionsFullTrackCsv = xlsxParsingService.ParseXlsx<CadetPromotionsFullTrackCsv>(file.OpenReadStream());
        await processDataService.ProcessAsync(cadetPromotionsFullTrackCsv);

        return Ok(new { success = "File successfully uploaded and processed" });
    }
}