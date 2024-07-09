using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_API.Models;

public class Flight
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
