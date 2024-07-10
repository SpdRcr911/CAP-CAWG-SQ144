namespace CAPSquadron_API.Models;

public class FlightDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public FlightMemberDto? FlightCommander { get; set; }
    public List<FlightMemberDto> FlightSergeants { get; set; } = new List<FlightMemberDto>();
    public List<FlightMemberDto> Members { get; set; } = new List<FlightMemberDto>();
}
