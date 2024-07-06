using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class Achievement(string nameLast, string nameFirst, string email, string achvName, string region, string wing, string unit)
{
    public int Id { get; set; }

    [Required]
    public int CAPID { get; set; }

    [Required]
    public string NameLast { get; set; } = nameLast;

    [Required]
    public string NameFirst { get; set; } = nameFirst;

    [Required]
    public string Email { get; set; } = email;

    [Required]
    public string AchvName { get; set; } = achvName;

    public DateOnly? AprDate { get; set; }

    public DateOnly JoinDate { get; set; }

    [Required]
    public string Region { get; set; } = region;

    [Required]
    public string Wing { get; set; } = wing;

    [Required]
    public string Unit { get; set; } = unit;

    public DateOnly? PhyFitTest { get; set; }

    public DateOnly? LeadLabDateP { get; set; }

    public int? LeadLabScore { get; set; }

    public DateOnly? AEDateP { get; set; }

    public int? AEScore { get; set; }

    public string? AEModuleOrTest { get; set; }

    public string? CharacterDevelopment { get; set; }

    public bool ActivePart { get; set; }

    public DateOnly? ActiveParticipationDate { get; set; }

    public bool CadetOath { get; set; }

    public DateOnly? CadetOathDate { get; set; }

    public DateOnly? LeadershipExpectationsDate { get; set; }

    public DateOnly? UniformDate { get; set; }

    public DateOnly? SpecialActivityDate { get; set; }

    public DateOnly? NextApprovalDate { get; set; }

    public DateOnly? StaffServiceDate { get; set; }

    public DateOnly? OralPresentationDate { get; set; }

    public DateOnly? TechnicalWritingAssignmentDate { get; set; }

    public string? TechnicalWritingAssignment { get; set; }

    public DateOnly? DrillDate { get; set; }

    public int? DrillScore { get; set; }

    public DateOnly? WelcomeCourseDate { get; set; }

    public DateOnly? EssayDate { get; set; }

    public DateOnly? SpeechDate { get; set; }

    public DateOnly? AEInteractiveDate { get; set; }

    public string? AEInteractiveModule { get; set; }

    public DateOnly? LeadershipInteractiveDate { get; set; }

    public DateOnly LastModified { get; set; }
}