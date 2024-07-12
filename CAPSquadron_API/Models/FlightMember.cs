using System.ComponentModel.DataAnnotations;

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

    public Flight? Flight { get; set; }

    public AttendanceSignIn? Member { get; set; }
}
