
namespace CAPSquadron_WebServer.Services
{
    public interface IMemberServcie
    {
        public Task<IEnumerable<Member>> GetCadetsAsync();
        public Task<IEnumerable<Member>> GetSeniorMembersAsync();
    }

    public class MemberService(ApiClient apiClient) : IRetrieveDataService<Member>, IMemberServcie
    {
        public async Task<IEnumerable<Member>> GetAsync()
        {
            return await apiClient.GetMembersAsync();
        }

        public async Task<Member> GetAsync(int id)
        {
            return await apiClient.GetMemberByCapIdAsync(id);
        }

        public async Task<IEnumerable<Member>> GetAsync(IEnumerable<int> ids)
        {
            return await apiClient.GetMembersByCapidsAsync(ids);
        }

        public async Task<IEnumerable<Member>> GetCadetsAsync()
        {
            return await apiClient.GetCadetsAsync();
        }
        public async Task<IEnumerable<Member>> GetSeniorMembersAsync()
        {
            return await apiClient.GetSeniorMembersAsync();
        }
    }
}
