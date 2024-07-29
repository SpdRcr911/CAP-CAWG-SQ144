using Microsoft.AspNetCore.Components;

namespace CAPSquadron_WebServer.Components;

public partial class PromotionProgressBar
{
    [Parameter]
    public DateTime NextPromotionDate { get; set; }

    private int CurrentProgress => CalculateProgress();
    private int ProgressLeft => Math.Max(0, 56 - CurrentProgress);
    private int OverdueDays => CalculateOverdueDays();
    private int ProgressPercent => CalculateProgressPercent();
    private (int Normal, int Overdue) OverdueProgressPercent => CalculateOverdueProgressPercent();

    private DateTime StartDate => NextPromotionDate.AddDays(-56); // 8 weeks before the next promotion date

    private int CalculateProgress()
    {
        int elapsedDays = (DateTime.Now - StartDate).Days;
        return Math.Min(elapsedDays, 56);
    }

    private int CalculateProgressPercent()
    {
        int elapsedDays = (DateTime.Now - StartDate).Days;
        return (elapsedDays >= 56) ? 100 : (int)((double)elapsedDays / 56 * 100);
    }

    private (int Normal, int Overdue) CalculateOverdueProgressPercent()
    {
        int total = OverdueDays + CurrentProgress;
        int normalPercent = (int)Math.Round((double)CurrentProgress / total * 100);
        int overduePercent = 100 - normalPercent;
        return (normalPercent, overduePercent);
    }

    private int CalculateOverdueDays()
    {
        DateTime currentDate = DateTime.Now;
        return currentDate > NextPromotionDate ? (int)(currentDate - NextPromotionDate).TotalDays : 0;
    }

    private string GetPromotionDifferenceMessage()
    {
        int diff = (NextPromotionDate - DateTime.Now).Days;
        return diff switch
        {
            > 0 => $"{diff} days until the next promotion date",
            < 0 => $"{Math.Abs(diff)} days past your promotion date",
            0 => "You can promote today"
        };
    }
}
