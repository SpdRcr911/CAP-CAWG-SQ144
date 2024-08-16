using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary;

public partial class TaskDisplay
{
    [Parameter]
    public bool TaskStatus { get; set; }

    [Parameter]
    public DateTimeOffset? Date1 { get; set; }

    [Parameter]
    public DateTimeOffset? Date2 { get; set; }

    private string GetDateDisplay(DateTimeOffset? date) => date.HasValue ? date.Value.LocalDateTime.ToShortDateString() : "-";
}
