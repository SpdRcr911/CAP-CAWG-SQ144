using CAPSquadron_Shared.Services;

namespace CAPSquadron_Shared.Models;

public class FlightMembership
{
    public int FlightId { get; set; }
    public Member? FlightCommander { get; set; }
    public List<Member> FlightSergeants { get; set; } = new();
    public List<Member> Members { get; set; } = new();
}
