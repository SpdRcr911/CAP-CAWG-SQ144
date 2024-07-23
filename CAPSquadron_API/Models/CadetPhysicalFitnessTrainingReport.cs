using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class CadetPhysicalFitnessTrainingReport
{
    public int Id { get; set; }
    [Required]
    public int CAPID { get; set; }
    public string? FullName { get; set; }
    public string? Rank { get; set; }
    public string? HFZCred { get; set; }
    public int PacerRunReq { get; set; }
    public TimeSpan MileRunReq { get; set; }
    public int CurlUpReq { get; set; }
    public int PushUpReq { get; set; }
    public int SitAndReachReq { get; set; }
    public DateOnly? Expiration { get; set; }
    public DateTimeOffset RecordTimeStamp { get; set; }
}
