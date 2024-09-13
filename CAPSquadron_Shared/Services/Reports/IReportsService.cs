namespace CAPSquadron_Shared.Services.Reports;

public interface IReportsService
{
    Task<IEnumerable<PhaseSummaryDto>> GetPhaseReportAsync();
    Task<IEnumerable<AchievementSummaryDto>> GetAchievementReportAsync(string phase);
}

public class ReportsService(ApiClient apiClient) : IReportsService
{
    public async Task<IEnumerable<PhaseSummaryDto>> GetPhaseReportAsync()
    {
        return await apiClient.PhasesAsync();
    }
    public async Task<IEnumerable<AchievementSummaryDto>> GetAchievementReportAsync(string phase)
    {
        return await apiClient.AchievementsAsync(phase);
    }
    
}
