namespace CAPSquadron_SPA.Models;

public class Member
{
    public int CAPID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Rank { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public bool EoCompleted { get; set; }
    public bool OpsecCompleted { get; set; }
    public bool SafetyCurrent { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime? InactiveDate { get; set; }
    public int? FlightId { get; set; }
    public bool IsExecutiveStaff { get; set; }
    public bool IsOnLeave { get; set; }
}
