namespace CAPSquadron_API.Models;

public class CadetDetailsDto
{
    public int Capid { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public string Status { get; set; }
}
