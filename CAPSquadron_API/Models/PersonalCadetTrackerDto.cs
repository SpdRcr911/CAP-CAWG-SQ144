using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class PersonalCadetTrackerDto
{
    [Column("capid")]
    public int CAPID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime? NextApprovalDate { get; set; }
    public bool? LeadershipTask1 { get; set; }
    public bool? LeadershipTask2 { get; set; }
    public bool? Aerospace { get; set; }
    public bool? Fitness { get; set; }
    public bool? Character { get; set; }
    public bool? SpecialRequirement { get; set; }
    public double? LeadLabScore { get; set; }
    public DateTime? LeadLabDate { get; set; }
    public double? DrillScore { get; set; }
    public DateTime? DrillDate { get; set; }
    public DateTime? Aedate { get; set; }
    public double? Aescore { get; set; }
    public string? AemoduleOrTest { get; set; }
    public DateTime? PhyFitTest { get; set; }
}
