using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class CallDownResponse
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CapId { get; set; }
    [Required]
    public DateOnly MeetingDate { get; set; }
    public bool? Attending { get; set; } = false;
    public string? Reason { get; set; }
    public List<string> Requests { get; set; } = [];
    public string? Comments { get; set; }
}
