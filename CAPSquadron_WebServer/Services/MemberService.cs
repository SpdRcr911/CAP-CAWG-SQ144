
namespace CAPSquadron_WebServer.Services
{
    public class MemberService(ApiClient apiClient) : IRetrieveDataService<Member>
    {
        public async Task<IEnumerable<Member>> GetAsync()
        {
            return await apiClient.GetMembersAsync();
        }

        public async Task<Member> GetAsync(int id)
        {
            return await apiClient.GetMemberAsync(id);
        }

        public async Task<IEnumerable<Member>> GetAsync(IEnumerable<int> ids)
        {
            return await apiClient.GetMembersByCapidsAsync(ids);
        }
    }
}
