namespace CAPSquadron_Shared.Services.MemberAttribute;

public interface IMemberAttributeService
{
    Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync();
    Task<MemberAttributesDto> GetMemberAttributesByCapIdAsync(int capid);
}
