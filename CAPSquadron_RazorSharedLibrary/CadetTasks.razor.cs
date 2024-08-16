using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary;

public partial class CadetTasks
{
    [Parameter]
    public bool? LearnToLead { get; set; }
    [Parameter]
    public DateTimeOffset? LeadLabDate { get; set; }
    [Parameter]
    public DateTimeOffset? LeadInteractiveDate { get; set; }
    [Parameter]
    public bool? Aerospace { get; set; }
    [Parameter]
    public DateTimeOffset? AeDate { get; set; }
    [Parameter]
    public DateTimeOffset? AeInteractiveDate { get; set; }
    [Parameter]
    public bool? DrillAndCeremonies { get; set; }
    [Parameter]
    public DateTimeOffset? DrillDate { get; set; }
    [Parameter]
    public bool? Fitness { get; set; }
    [Parameter]
    public DateTimeOffset? PhyFitTest { get; set; }
    [Parameter]
    public bool? Character { get; set; }
    [Parameter]
    public DateTimeOffset? NextApprovalDate { get; set; }

    private bool TimeInGradeComplete => NextApprovalDate.HasValue && NextApprovalDate.Value <= DateTime.Now;
    private string GetStatusIcon(bool taskCompleted) => taskCompleted ? "✅ " : "❌ ";
    private string NextPromotionDifference(DateTimeOffset NextPromotionDate)
    {
        DateTime currentDate = DateTime.Now;
        var diff = (NextPromotionDate - currentDate).Days;

        return diff switch
        {
            > 0 => $"{diff} days until next promotion date",
            < 0 => $"{Math.Abs(diff)} days past your promition date",
            0 => "can promote today"
        };

    }
}
