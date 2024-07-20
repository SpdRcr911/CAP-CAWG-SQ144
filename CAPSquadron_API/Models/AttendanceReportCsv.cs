using CsvHelper.Configuration.Attributes;

namespace CAPSquadron_API.Models;

public class AttendanceReportCsv
{
    public DateTime? StartDate { get; set; }
    [Ignore]
    public string? Topics { get; set; }
    public string? TxtDrillAndCeremony { get; set; }
    public string? TxtAET { get; set; }
    public string? Topic_PhysicalFitnessTesting { get; set; }
    public string? TxtMemberType { get; set; }
    [Ignore]
    public string? TextMemberType { get; set; }
    [Ignore]
    public string? MemberType { get; set; }
    public string? TxtSBT { get; set; }
    public string? TextCadetPrograms { get; set; }
    public string? TextCharacterDevelopment { get; set; }
    [Ignore]
    public string? TxtStaffDutyAssignment { get; set; }
    [Ignore]
    public string? TextProfessionalDevelopment { get; set; }
    public string? TextCommunityService { get; set; }
    public string? Location { get; set; }
    public string? TxtOther { get; set; }
    public string? TextOther { get; set; }
    public string? SECTION { get; set; }
    public string? Name { get; set; }
    public string? TxtAddress { get; set; }
    public string? TxtPhone { get; set; }
    public string? TxtWebsite { get; set; }
    public string? TxtMeetingInfo { get; set; }
    public string? FullName { get; set; }
    public string? Rank { get; set; }
    public int? CAPID { get; set; }
    public DateOnly? Expiration { get; set; }
    public string? Present { get; set; }
    public string? Excused { get; set; }
    public string? Uniform { get; set; }
    public string? TxtCAPF160_161 { get; set; }
    public string? TxtEO { get; set; }
    public string? TxtOPSEC { get; set; }
    public string? Safety { get; set; }
    public string? GstName { get; set; }
    public string? GstRank { get; set; }
    public string? GstPhone { get; set; }
    public string? GstEmail { get; set; }
    public string? GstNotes { get; set; }
    public string? Textbox3 { get; set; }
}
