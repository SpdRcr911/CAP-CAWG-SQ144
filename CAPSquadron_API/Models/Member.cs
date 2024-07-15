using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class Member
{
    [Key]
    public int ID { get; set; }
    public string? Region1 { get; set; }
    public string? Wing_Unit { get; set; }
    public string? FullName { get; set; }
    [Required]
    public int CAPID { get; set; }
    public string? Rank { get; set; }
    public DateOnly RankDate { get; set; }
    public string? Gender { get; set; }
    public DateOnly Joined { get; set; }
    public DateOnly Expiration { get; set; }
    public string? HPhone { get; set; }
    public string? CPhone { get; set; }
    public string? Address { get; set; }
    public string? EmailAddress { get; set; }
    public string? CityStateZip { get; set; }
    public string? FBIStatus { get; set; }
}
