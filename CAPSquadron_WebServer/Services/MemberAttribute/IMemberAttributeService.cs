namespace CAPSquadron_WebServer.Services.MemberAttribute;

public interface IMemberAttributeService
{
    Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync();
    Task<MemberAttributesDto> GetMemberAttributesByCapIdAsync(int capid);
}
