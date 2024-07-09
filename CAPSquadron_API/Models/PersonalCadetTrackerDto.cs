using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class PersonalCadetTrackerDto
{
    [Column("capid")]
    public int CAPID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public string? AchievementName { get; set; }
    public DateTime? NextApprovalDate { get; set; }
    public bool? LeardToLead { get; set; }
    public bool? DrillAndCeremonies { get; set; }
    public bool? Aerospace { get; set; }
    public bool? Fitness { get; set; }
    public bool? Character { get; set; }
    public DateTime? CharacterDevelopmentDate { get; set; }
    public DateTime? WelcomeCourseDate { get; set; }
    public bool? SpecialRequirement { get; set; }
    public bool? HonorCredit { get; set; }
    public double? LeadLabScore { get; set; }
    public DateTime? LeadLabDate { get; set; }
    public DateTime? LeadInteractiveDate { get; set; }
    public double? DrillScore { get; set; }
    public DateTime? DrillDate { get; set; }
    public DateTime? AEDate { get; set; }
    public double? AEScore { get; set; }
    public string? AEModuleOrTest { get; set; }
    public string? AEInteractiveModule { get; set; }
    public DateTime? AEInteractiveDate { get; set; }
    public DateTime? PhyFitTest { get; set; }
    public DateTimeOffset? LastModified { get; set; }
}
