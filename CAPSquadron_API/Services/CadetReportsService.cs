using CAPSquadron_API.Data;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class CadetReportsService(QueryDbContext queryDbContext) : ICadetReportsService
{
    public async Task<IEnumerable<PhaseSummaryDto>> GetPhaseSummaryAsync()
    {
        var query = @"
                WITH phase_summary AS (
                    SELECT 
                        cadet_achievements.phase, 
                        COUNT(DISTINCT cadet_promotions_full_tracks.capid) AS total_cadets,
                        SUM(CASE WHEN apr_date IS NOT NULL THEN 1 ELSE 0 END) AS completed,
                        SUM(CASE WHEN apr_date IS NULL THEN 1 ELSE 0 END) AS in_progress,
                        SUM(
                            CASE 
                                WHEN apr_date IS NOT NULL 
                                     AND apr_date < (CURRENT_DATE - INTERVAL '8 weeks') 
                                     AND apr_date = (
                                        SELECT MAX(sub.apr_date)
                                        FROM public.cadet_promotions_full_tracks sub
                                        JOIN cadet_achievements sub_ach ON sub.achv_name = sub_ach.achievement_name
                                        WHERE sub.capid = cadet_promotions_full_tracks.capid 
                                          AND sub_ach.phase = cadet_achievements.phase
                                          AND sub.apr_date IS NOT NULL
                                    )
                                     AND cadet_promotions_full_tracks.capid IN (
                                        SELECT sub2.capid 
                                        FROM public.cadet_promotions_full_tracks sub2
                                        JOIN cadet_achievements sub2_ach ON sub2.achv_name = sub2_ach.achievement_name
                                        WHERE sub2_ach.phase = cadet_achievements.phase 
                                          AND sub2.apr_date IS NULL
                                    )
                                THEN 1 
                                ELSE 0 
                            END
                        ) AS overdue_promotions
                    FROM 
                        public.cadet_promotions_full_tracks
                    JOIN 
                        cadet_achievements ON cadet_promotions_full_tracks.achv_name = cadet_achievements.achievement_name
                    GROUP BY 
                        cadet_achievements.phase
                )
                SELECT 
                    phase, 
                    total_cadets AS ""Total Cadets"", 
                    completed, 
                    in_progress AS ""In Progress"",
                    overdue_promotions AS ""Overdue Promotions""
                FROM 
                    phase_summary
                ORDER BY 
                    phase;
            ";

        return await queryDbContext.PhaseSummaries.FromSqlRaw(query).ToListAsync();
    }

    public async Task<IEnumerable<AchievementSummaryDto>> GetAchievementSummaryAsync(string phase)
    {
        var query = @"
            WITH last_promotion AS (
                SELECT 
                    cpf.capid,
                    MAX(cpf.apr_date) AS last_apr_date,
                    MAX(cpf.apr_date) < (CURRENT_DATE - INTERVAL '8 weeks') AS overdue
                FROM 
                    public.cadet_promotions_full_tracks cpf
                WHERE
                    cpf.apr_date IS NOT NULL
                GROUP BY 
                    cpf.capid
            )
            SELECT 
                cadet_achievements.phase, 
                cadet_achievements.achievement_name AS ""Achievement Name"", 
                cadet_achievements.rank, 
                COUNT(DISTINCT cpf.capid) AS ""Total Cadets"",
                SUM(CASE WHEN cpf.apr_date IS NOT NULL THEN 1 ELSE 0 END) AS completed,
                SUM(CASE WHEN cpf.apr_date IS NULL THEN 1 ELSE 0 END) AS ""In Progress"",
                SUM(CASE WHEN oc.overdue AND cpf.apr_date IS NULL THEN 1 ELSE 0 END) AS ""Overdue Promotions""
            FROM 
                public.cadet_promotions_full_tracks cpf
            JOIN 
                cadet_achievements ON cpf.achv_name = cadet_achievements.achievement_name
            LEFT JOIN 
                last_promotion oc ON cpf.capid = oc.capid
            WHERE 
                cadet_achievements.phase = @p0
            GROUP BY 
                cadet_achievements.phase, cadet_achievements.id, cadet_achievements.achievement_name, cadet_achievements.rank
            ORDER BY 
                cadet_achievements.id;

            ";

        return await queryDbContext.AchievementSummaries.FromSqlRaw(query, phase).ToListAsync();
    }

    public async Task<IEnumerable<CadetDetailsDto>> GetCadetDetailsAsync(string phase, string achievementName)
    {
        var query = @"
            WITH last_promotion AS (
                SELECT 
                    cpf.capid,
                    MAX(cpf.apr_date) AS last_apr_date,
                    MAX(cpf.apr_date) < (CURRENT_DATE - INTERVAL '8 weeks') AS overdue
                FROM 
                    public.cadet_promotions_full_tracks cpf
                WHERE
                    cpf.apr_date IS NOT NULL
                GROUP BY 
                    cpf.capid
            ),
            cadet_details AS (
                SELECT 
                    cadet_achievements.phase, 
                    cadet_achievements.achievement_name, 
                    cpf.capid, 
                    cpf.name_last, 
                    cpf.name_first, 
                    cpf.email, 
                    cpf.apr_date,
                    CASE 
                        WHEN cpf.apr_date IS NOT NULL THEN 'Completed' 
                        ELSE 'In Progress' 
                    END AS status,
                    CASE 
                        WHEN lp.overdue AND cpf.apr_date IS NULL THEN 'Overdue' 
                        ELSE NULL 
                    END AS overdue_status
                FROM 
                    public.cadet_promotions_full_tracks cpf
                JOIN 
                    cadet_achievements ON cpf.achv_name = cadet_achievements.achievement_name
                LEFT JOIN 
                    last_promotion lp ON cpf.capid = lp.capid
                WHERE 
                    cadet_achievements.phase = @p0
                    AND cadet_achievements.achievement_name = @p1
            )
            SELECT 
                capid, 
	            achievement_name,
                name_last AS ""LastName"", 
                name_first AS ""FirstName"", 
                email, 
                apr_date AS ""ApprovalDate"", 
                status,
                overdue_status
            FROM 
                cadet_details
            WHERE
	            status = 'In Progress'
            ORDER BY 
                name_last, name_first;

            ";

        return await queryDbContext.CadetDetails.FromSqlRaw(query, phase, achievementName).ToListAsync();
    }
}
