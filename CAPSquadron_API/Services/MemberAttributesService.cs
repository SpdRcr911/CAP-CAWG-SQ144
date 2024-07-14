﻿using CAPSquadron_API.Data;
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
                WITH WrightBrothersCTE AS (
                    SELECT DISTINCT capid
                    FROM achievements
                    WHERE achv_name = 'Wright Brothers'
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                ),
                CurryAchievementCTE AS (
                    SELECT DISTINCT capid
                    FROM achievements
                    WHERE achv_name = 'Achievement 1' AND apr_date <> NULL
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                ),
                CawgcapEmailCTE AS (
                    SELECT DISTINCT capid
                    FROM achievements
                    WHERE email LIKE '%@cawgcap.org'
                    {(capidCondition is not null ? $"AND capid = {capidCondition}" : "")}
                )
                SELECT 
                    asi.capid AS ""CAPID"",
                    asi.""name"" AS ""Name"",
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
                        WHEN email.capid IS NOT NULL THEN true
                        ELSE false
                    END AS ""HasCawgcapEmail""
                FROM 
                    attendance_sign_ins asi
                LEFT JOIN 
                    WrightBrothersCTE wb ON asi.capid = wb.capid
                LEFT JOIN 
                    CurryAchievementCTE curry ON asi.capid = curry.capid
                LEFT JOIN 
                    CawgcapEmailCTE email ON asi.capid = email.capid
                LEFT JOIN 
                    general_emergency_services ges ON asi.capid = ges.capid
                {(capidCondition is not null ? $"WHERE asi.capid = {capidCondition}" : "")}
                ORDER BY 
                    asi.""name""
            ";
        }
    }
}