using Microsoft.AspNetCore.Components;
using CAPSquadron_WebServer.Services;

namespace CAPSquadron_WebServer.Components;

public partial class CadetTrackerComponent
{
    [Parameter]
    public IEnumerable<PersonalCadetTrackerDto>? CadetTrackers { get; set; }

    [Parameter]
    public IEnumerable<MemberAttributesDto>? MemberAttributes { get; set; }

    private string DisplayTask(bool? taskCompleted, DateTimeOffset? dateCompleted)
    {
        if (taskCompleted.HasValue && dateCompleted.HasValue)
            return dateCompleted.Value.LocalDateTime.ToShortDateString();

        return "-";
    }
}
