using CAPSquadron_API.Data;
using CAPSquadron_API.Extensions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class CadetTrackerService : ICadetTrackerService
{
    private readonly QueryDbContext _queryDbContext;

    public CadetTrackerService(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task<IEnumerable<PersonalCadetTrackerDto>> GetCadetTrackerAsync()
    {
        var query = BuildQuery();
        return await _queryDbContext.PersonalCadetTrackers.FromSqlRaw(query).ToListAsync();
    }

    public async Task<PersonalCadetTrackerDto?> GetCadetTrackerByCapidAsync(int capid)
    {
        var query = BuildQuery("capid = @p0");
        return await _queryDbContext.PersonalCadetTrackers.FromSqlRaw(query, capid).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PersonalCadetTrackerDto>?> GetCadetTrackerByCapidsAsync(IEnumerable<int> capids)
    {
        var capidsList = string.Join(",", capids);
        var query = BuildQuery($"capid IN ({capidsList})");
        return await _queryDbContext.PersonalCadetTrackers.FromSqlRaw(query).ToListAsync();
    }

    public async Task<IEnumerable<PersonalCadetTrackerDto>?> GetCadetTrackerByAchvNameAsync(string achvName)
    {
        var query = BuildQuery("achv_name = @p0");
        return await _queryDbContext.PersonalCadetTrackers.FromSqlRaw(query, achvName).ToListAsync();
    }

    private string BuildQuery(string? additionalCondition = null)
    {
        var baseQuery = @"
                SELECT capid, name_last AS ""LastName"", name_first AS ""FirstName"", email AS ""Email"",
                next_approval_date AS ""NextApprovalDate"", achv_name AS ""AchievementName"",
                CASE WHEN lead_lab_date_p is not null or leadership_interactive_date is not null THEN true ELSE false END AS ""LeardToLead"",
                CASE WHEN achv_name = 'Billy Mitchell' THEN null
                    WHEN drill_date is not null THEN true ELSE false END AS ""DrillAndCeremonies"",
                CASE WHEN achv_name = 'Achievement 1' OR achv_name = 'Wright Brothers' THEN null
                    WHEN aedate_p is not null or aeinteractive_date is not null THEN true ELSE false END AS ""Aerospace"",
                CASE WHEN phy_fit_test is not null THEN true ELSE false END AS ""Fitness"",
                CASE WHEN achv_name = 'Wright Brothers' OR achv_name = 'Billy Mitchell' THEN null
                    WHEN character_development is not null THEN true ELSE false END AS ""Character"",
                CASE WHEN achv_name = 'Achievement 1' AND welcome_course_date is not null THEN true
                    WHEN achv_name = 'Achievement 1' AND welcome_course_date is null THEN false
                    ELSE null
                END AS ""SpecialRequirement"",
				CASE WHEN leadership_interactive_date is not null and aeinteractive_date is not null THEN true ELSE false END AS ""HonorCredit"",
                lead_lab_score AS ""LeadLabScore"", lead_lab_date_p AS ""LeadLabDate"", leadership_interactive_date AS ""LeadInteractiveDate"",
                aedate_p AS ""AEDate"", aescore AS ""AEScore"", aemodule_or_test AS ""AEModuleOrTest"", aeinteractive_date AS ""AEInteractiveDate"", aeinteractive_module AS ""AEInteractiveModule"",
                drill_score AS ""DrillScore"", drill_date AS ""DrillDate"",
                phy_fit_test AS ""PhyFitTest"",
                character_development AS ""CharacterDevelopmentDate"",
                welcome_course_date AS ""WelcomeCourseDate"",
		        last_modified AS ""LastModified""
                FROM public.cadet_promotions_full_tracks
                WHERE apr_date is null";

        if (!string.IsNullOrEmpty(additionalCondition))
        {
            baseQuery += $" AND {additionalCondition}";
        }

        baseQuery += " ORDER BY id ASC";
        return baseQuery;
    }
}
