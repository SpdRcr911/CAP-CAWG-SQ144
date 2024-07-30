using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CAPSquadron_API.Services
{
    public class MemberAttributesService(QueryDbContext context) : IMemberAttributesService
    {
        public async Task<IEnumerable<MemberAttributesDto>> GetMemberAttributesAsync(CancellationToken cancellationToken = default)
        {
            var query = GetBaseQuery();
            return await context.MemberAttributes.FromSqlRaw(query).ToListAsync(cancellationToken);
        }

        public async Task<MemberAttributesDto> GetMemberAttributesByCapidAsync(int capid, CancellationToken cancellationToken = default)
        {
            var query = GetBaseQuery(capid);
            return await context.MemberAttributes.FromSqlRaw(query).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException($"Member attributes not found for {capid}");
        }

        private static string GetBaseQuery(int? capidCondition = null)
        {
            return $@"
                WITH WIPAchivement AS (
	                  SELECT DISTINCT capid, achv_name
	                  FROM cadet_promotions_full_tracks
	                  WHERE apr_date IS NULL
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                ),
                WrightBrothersCTE AS (
                    SELECT DISTINCT capid
                    FROM cadet_promotions_full_tracks
                    WHERE achv_name = 'Wright Brothers'
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                ),
                CurryAchievementCTE AS (
                    SELECT DISTINCT capid
                    FROM cadet_promotions_full_tracks
                    WHERE achv_name = 'Achievement 1' AND apr_date IS NOT NULL
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                )
                SELECT 
                    mem.capid AS ""CAPID"",
	                mem.rank as ""Rank"",
	                mem.""full_name"" AS ""Name"",
	                wip.achv_name AS ""WIPAchName"",
                    CASE 
                        WHEN wb.capid IS NOT NULL THEN true
                        ELSE false
                    END AS ""HasWrightBrothersAchievement"",
                    CASE 
                        WHEN ges.capid IS NOT NULL THEN true
                        ELSE false
                    END AS ""HasGESCertification"",
                    CASE 
                        WHEN curry.capid IS NOT NULL THEN true
                        ELSE false
                    END AS ""HasCurryAchievement"",
	                CASE
		                WHEN mem.email_address LIKE '%@cawgcap.org' THEN true
		                ELSE false
	                END AS ""HasCawgcapEmail"",
	                CASE
		                WHEN EXTRACT(YEAR FROM expiration) = EXTRACT(YEAR FROM NOW()) 
		                 AND EXTRACT(MONTH FROM expiration) = EXTRACT(MONTH FROM NOW()) THEN false
		                ELSE true
	                END AS ""NotExpiringThisMonth""
                FROM 
                    (select * FROM members WHERE ""rank"" like 'C/%') mem
                LEFT JOIN 
                    WrightBrothersCTE wb ON mem.capid = wb.capid
                LEFT JOIN 
                    CurryAchievementCTE curry ON mem.capid = curry.capid
                LEFT JOIN
	                WIPAchivement wip ON mem.capid = wip.capid
                LEFT JOIN 
                    general_emergency_services ges ON mem.capid = ges.capid
                {(capidCondition is not null ? $"WHERE mem.capid = {capidCondition}" : "")}
                ORDER BY 
                    mem.""full_name""
            ";
        }
    }
}
