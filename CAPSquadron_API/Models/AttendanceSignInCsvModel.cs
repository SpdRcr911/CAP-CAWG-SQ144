namespace CAPSquadron_API.Models;
public class AttendanceSignInCsvModel(int capid, string member, string rank, DateOnly expiration)
{
    public string Member { get; set; } = member;
    public string Rank { get; set; } = rank;
    public int CAPID { get; set; } = capid;
    public DateOnly Expiration { get; set; } = expiration;
}
