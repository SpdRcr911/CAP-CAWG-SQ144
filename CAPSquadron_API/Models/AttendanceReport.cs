using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class AttendanceReport
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset? StartDate { get; set; }
    public string? Topics { get; set; }
    public bool IsDrillAndCeremony { get; set; }
    public bool IsAET { get; set; }
    public bool IsPhysicalFitnessTesting { get; set; }
    public bool IsSBT { get; set; }
    public bool IsCadetPrograms { get; set; }
    public bool IsCharacterDevelopment { get; set; }
    public bool IsCommunityService { get; set; }
    public string? Location { get; set; }
    public bool IsOther { get; set; }
    public string? Other { get; set; }
    public string? Section { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? MeetingInfo { get; set; }
    public string? FullName { get; set; }
    public string? Rank { get; set; }
    public int CAPID { get; set; }
    public DateOnly? Expiration { get; set; }
    public bool IsPresent { get; set; }
    public bool IsExcused { get; set; }
    public bool HasUniform { get; set; }
    public bool HasCAPF160_161 { get; set; }
    public bool EOCurrent { get; set; }
    public bool OPSECCurrent { get; set; }
    public bool SafetyCurrent { get; set; }
    public string? GuestName { get; set; }
    public string? GuestRank { get; set; }
    public string? GuestPhone { get; set; }
    public string? GuestEmail { get; set; }
    public string? GuestNotes { get; set; }
    public string? OverallNotes { get; set; }
}
