﻿
namespace CAPSquadron_Shared.Services.MemberAttribute
{
    public class MemberAttributeService(ApiClient apiClient) : IMemberAttributeService
    {
        public async Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync()
        {
            return await apiClient.GetMemberAttributesAsync();
        }

        public async Task<MemberAttributesDto> GetMemberAttributesByCapIdAsync(int capid)
        {
            try
            {
                return await apiClient.GetMemberAttributesByCapidAsync(capid);
            }
            catch (ApiException) {
                return new MemberAttributesDto();
            }
        }
    }
}
