using CAPSquadron_Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CAPSquadron_RazorSharedLibrary;

public partial class MemberAttributesComponent
{
    [Parameter]
    public MemberAttributesDto? Attributes { get; set; }
    private int currentBadge = 0;

    private List<KeyValuePair<string, bool>> GetAttributesList()
    {
        if (Attributes is null)
            return [];

        return new List<KeyValuePair<string, bool>>
    {
        new ("Email", Attributes.HasCawgcapEmail),
        new ("GES", Attributes.HasGESCertification),
        new ("Curry", Attributes.HasCurryAchievement),
        new ("WB", Attributes.HasWrightBrothersAchievement),
        new ("MEM", Attributes.NotExpiringThisMonth)
    };
    }

    private string GetBadgeClass(bool attributeValue)
    {
        return attributeValue ? "bg-success" : "bg-danger";
    }
    private string GetBadgeMargin()
    {
        return currentBadge++ % 2 == 0 ? "mx-0" : "mx-1";
    }
}
