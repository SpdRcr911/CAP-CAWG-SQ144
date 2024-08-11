using System.ComponentModel.DataAnnotations;

namespace CAPSquadron_Cadet.Models;

public class Cadet
{
    [Required]
    public string? CAPID { get; set; }
}