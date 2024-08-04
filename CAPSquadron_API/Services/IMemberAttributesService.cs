using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface IMemberAttributesService
{
    Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync(CancellationToken cancellationToken = default);
    Task<MemberAttributesDto?> GetMemberAttributesByCapidAsync(int capid, CancellationToken cancellationToken = default);

}
