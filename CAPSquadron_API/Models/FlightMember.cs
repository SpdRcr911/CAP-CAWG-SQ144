using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class FlightMember
{
    public int Id { get; set; }

    [Required]
    public int FlightId { get; set; }

    [Required]
    public int CAPID { get; set; }

    public bool IsFlightCommander { get; set; }

    public bool IsFlightSergeant { get; set; }

    [ForeignKey("FlightId")]
    public Flight? Flight { get; set; }

    [ForeignKey("CAPID")]
    public Member? Member { get; set; }
}
