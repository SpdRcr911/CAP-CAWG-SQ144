using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class AchievementSummaryDto
{
    public string Phase { get; set; }
    [Column("Achievement Name")]
    public string AchievementName { get; set; }
    public string Rank { get; set; }
    [Column("Total Cadets")]
    public int TotalCadets { get; set; }
    public int Completed { get; set; }
    [Column("In Progress")]
    public int InProgress { get; set; }
    [Column("Overdue Promotions")]
    public int OverduePromotions { get; set; }
}
