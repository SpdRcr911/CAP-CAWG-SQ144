namespace CAPSquadron_Shared.Services;

public interface IValidateCadet
{
    Task<bool> ValidateAsync(int capId, DateOnly joinDate);
}

public class ValidateCadet(ApiClient apiClient) : IValidateCadet
{
    public async Task<bool> ValidateAsync(int capId, DateOnly joinDate)
    {
        try
        {
            var cadet = await apiClient.GetMemberByCapIdAsync(capId);
            if (cadet is not null)
            {
                return cadet.Joined == joinDate;
            }
            return false;
        }
        catch (ApiException ex) when (ex.StatusCode == 404)
        {

            return false;
        }
    }
}
