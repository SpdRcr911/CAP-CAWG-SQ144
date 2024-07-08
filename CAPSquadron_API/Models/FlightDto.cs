namespace CAPSquadron_API.Models;

public class FlightDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public FlightMemberDto? FlightCommander { get; set; }
    public List<FlightMemberDto>? FlightSergeants { get; set; }
    public List<FlightMemberDto>? Members { get; set; }
}
