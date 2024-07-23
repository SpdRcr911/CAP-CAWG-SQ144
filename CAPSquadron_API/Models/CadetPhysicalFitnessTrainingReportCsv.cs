using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class CadetPhysicalFitnessTrainingReportCsv
{
    [Column("HFZ Cred")]
    public string? HFZCred { get; set; }
    [Column("Full Name")]
    public string? FullName { get; set; }
    [Column("Pacer Run Req.")]
    public int PacerRunReq { get; set; }
    [Column("Mile Run Req.")]
    public string? MileRunReq { get; set; }
    [Column("Curl Up Req.")]
    public int CurlUpReq { get; set; }
    [Column("Push Up Req.")]
    public int PushUpReq { get; set; }
    [Column("Sit & Reach Req")]
    public int SitAndReachReq { get; set; }
    public string? Expiration { get; set; }
}
