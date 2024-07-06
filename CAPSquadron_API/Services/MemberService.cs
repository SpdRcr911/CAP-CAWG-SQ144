using CAPSquadron_API.Data;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class MemberService(AppDbContext context) : IRetrieveDataService<Member>, IProcessDataService<MemberCsvModel>
{
    private readonly AppDbContext _context = context;

    public async Task<List<Member>> GetAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task ProcessAsync(IEnumerable<MemberCsvModel> membersCsv)
    {
        var capIdsInCsv = membersCsv.Select(r => r.CAPID).ToHashSet();
        var members = await _context.Members.ToListAsync();
        var currentTime = DateTime.UtcNow;

        // Prepare lists for batch processing
        var membersToAdd = new List<Member>();
        var membersToUpdate = new List<Member>();

        // Mark all members not in CSV as inactive
        foreach (var member in members)
        {
            if (!capIdsInCsv.Contains(member.CAPID) && member.InactiveDate == null)
            {
                member.InactiveDate = currentTime;
                member.LastModified = currentTime;
                membersToUpdate.Add(member);
            }
        }

        foreach (var record in membersCsv)
        {
            var (Name, EoCompleted, OpsecCompleted, SafetyCurrent) = ParseMemberInfo(record.Member);
            var member = members.FirstOrDefault(m => m.CAPID == record.CAPID);

            if (member == null)
            {
                member = new Member(Name, record.Rank)
                {
                    CAPID = record.CAPID,
                    Expiration = record.Expiration,
                    EoCompleted = EoCompleted,
                    OpsecCompleted = OpsecCompleted,
                    SafetyCurrent = SafetyCurrent,
                    LastModified = currentTime
                };
                membersToAdd.Add(member);
            }
            else
            {
                member.Name = Name;
                member.Rank = record.Rank;
                member.Expiration = record.Expiration;
                member.EoCompleted = EoCompleted;
                member.OpsecCompleted = OpsecCompleted;
                member.SafetyCurrent = SafetyCurrent;
                member.LastModified = currentTime;
                member.InactiveDate = null;
                membersToUpdate.Add(member);
            }
        }

        // Batch processing
        if (membersToAdd.Any())
        {
            await _context.Members.AddRangeAsync(membersToAdd);
        }

        if (membersToUpdate.Any())
        {
            _context.Members.UpdateRange(membersToUpdate);
        }

        await _context.SaveChangesAsync();
    }

    private (string Name, bool EoCompleted, bool OpsecCompleted, bool SafetyCurrent) ParseMemberInfo(string member)
    {
        var eoCompleted = member.Contains('^');
        var opsecCompleted = member.Contains('*');
        var safetyCurrent = member.Contains('#');
        var name = member.Replace("^", "").Replace("*", "").Replace("#", "").Trim();
        return (name, eoCompleted, opsecCompleted, safetyCurrent);
    }
}