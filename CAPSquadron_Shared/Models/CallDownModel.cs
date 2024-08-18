namespace CAPSquadron_Shared.Models;

public class CallDownModel
{
    public bool Attending { get; set; } = false;
    public string? Reason { get; set; }
    public List<string> Requests { get; set; } = new List<string>(); // Initialize as an empty List<string>
    public string? Comments { get; set; }
}
