using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface ICadetReportsService
{
    Task<IEnumerable<PhaseSummaryDto>> GetPhaseSummaryAsync();
    Task<IEnumerable<AchievementSummaryDto>> GetAchievementSummaryAsync(string phase);
    Task<IEnumerable<CadetDetailsDto>> GetCadetDetailsAsync(string phase, string achievementName);
}
