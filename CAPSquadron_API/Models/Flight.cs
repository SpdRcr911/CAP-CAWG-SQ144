using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class Flight
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int? FlightCommanderCAPID { get; set; }
    public Member? FlightCommander { get; set; }

    public ICollection<Member> FlightSergeants { get; set; } = [];

    public ICollection<Member> Members { get; set; } = [];
}
