using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary;

public partial class HexagonComponent
{
    [Parameter]
    public bool? LearnToLead { get; set; }
    [Parameter]
    public bool? Aerospace { get; set; }
    [Parameter]
    public bool? DrillAndCeremonies { get; set; }
    [Parameter]
    public bool? Fitness { get; set; }
    [Parameter]
    public bool? Character { get; set; }
    [Parameter]
    public DateTimeOffset? NextApprovalDate { get; set; }
}
