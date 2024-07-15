namespace CAPSquadron_API.Models;

public class MemberCsv
{
    public string? Region1 { get; set; }
    public string? Wing_Unit { get; set; }
    public string? FullName { get; set; }
    public int CAPID { get; set; }
    public string? Rank { get; set; }
    public DateOnly RankDate { get; set; }
    public string? Gender1 { get; set; }
    public DateOnly Joined { get; set; }
    public DateOnly Expiration1 { get; set; }
    public string? HPhone { get; set; }
    public string? CPhone2 { get; set; }
    public string? Address { get; set; }
    public string? EmailAddress { get; set; }
    public string? CityStateZip { get; set; }
    public string? FBIStatus { get; set; }
}