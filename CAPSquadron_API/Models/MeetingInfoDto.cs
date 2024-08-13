namespace CAPSquadron_API.Models;

public class MeetingInfoDto
{
    public string? Title { get; set; }
    public DateOnly? Date { get; set; }
    public string? UOD { get; set; }
    public string? Topic { get; set; }
    public string? Notes { get; set; } = "";

}
