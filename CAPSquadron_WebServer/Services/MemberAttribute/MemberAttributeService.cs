
namespace CAPSquadron_WebServer.Services.MemberAttribute
{
    public class MemberAttributeService(ApiClient apiClient) : IMemberAttributeService
    {
        public async Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync()
        {
            return await apiClient.GetMemberAttributesAsync();
        }

        public async Task<MemberAttributesDto> GetMemberAttributesByCapIdAsync(int capid)
        {
            return await apiClient.GetMemberAttributesByCapidAsync(capid);
        }
    }
}
