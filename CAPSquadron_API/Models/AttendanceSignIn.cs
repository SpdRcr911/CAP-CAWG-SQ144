using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class AttendanceSignIn(string name, string rank)
{
    [Key]
    public int CAPID { get; set; }

    [Required]
    public string Name { get; set; } = name;

    public string Rank { get; set; } = rank;

    [Required]
    public DateOnly Expiration { get; set; }

    public bool EoCompleted { get; set; }

    public bool OpsecCompleted { get; set; }

    public bool SafetyCurrent { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public DateTimeOffset? InactiveDate { get; set; }

    public bool IsExecutiveStaff { get; set; }

    public bool IsOnLeave { get; set; }
}