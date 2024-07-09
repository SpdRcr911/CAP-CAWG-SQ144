using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class FlightDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? FlightCommanderId { get; set; }
    public List<int> FlightSergeantIds { get; set; } = [];
    public List<int> MemberIds { get; set; } = [];
}