using CAPSquadron_WebServer.Services;
using Microsoft.AspNetCore.Components;

namespace CAPSquadron_WebServer.Components;

public partial class MemberAttributesComponent
{
    [Parameter]
    public MemberAttributesDto? Attributes { get; set; }

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
        return attributeValue ? "text-bg-success" : "text-bg-danger";
    }
}
