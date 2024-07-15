using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CAPSquadron_API.Services;

public interface IMembershipService
{
    Task<IEnumerable<Member>> ParseCsvAsync(IFormFile file);
    Task UpdateMembershipListAsync(IEnumerable<Member> members);
}

public class MembershipService(AppDbContext context) : IRetrieveDataService<Member>, IProcessDataService<MemberCsv>
{
    public async Task<IEnumerable<Member>> GetAsync()
    {
        return await context.Members.ToListAsync();
    }

    public async Task<Member> GetAsync(int capid)
    {
        return await context.Members.FirstOrDefaultAsync(a => a.CAPID == capid) ?? throw new NotFoundException("Member not found.");
    }

    public async Task<IEnumerable<Member>> GetAsync(IEnumerable<int> ids)
    {
        return await context.Members.Where(a => ids.Contains(a.CAPID)).ToListAsync() ?? throw new NotFoundException("Members not found.");
    }


    public async Task ProcessAsync(IEnumerable<MemberCsv> members)
    {
        foreach (var member in members)
        {
            var existingMember = await context.Members.FirstOrDefaultAsync(m => m.CAPID == member.CAPID);
            if (existingMember is not null)
            {
                existingMember.Region1 = member.Region1;
                existingMember.Wing_Unit = member.Wing_Unit;
                existingMember.FullName = member.FullName;
                existingMember.Rank = member.Rank;
                existingMember.RankDate = member.RankDate;
                existingMember.Gender = member.Gender1;
                existingMember.Joined = member.Joined;
                existingMember.Expiration = member.Expiration1;
                existingMember.HPhone = member.HPhone;
                existingMember.CPhone = member.CPhone2;
                existingMember.Address = member.Address?.Replace("Address: ", "").Replace("\r\n","");
                existingMember.EmailAddress = member.EmailAddress?.Replace("Email: ","");
                existingMember.CityStateZip = member.CityStateZip;
                existingMember.FBIStatus = member.FBIStatus;
            }
            else
            {
                var newMember = new Member()
                {
                    CAPID = member.CAPID,
                    Region1 = member.Region1,
                    Wing_Unit = member.Wing_Unit,
                    FullName = member.FullName,
                    Rank = member.Rank,
                    RankDate = member.RankDate,
                    Gender = member.Gender1,
                    Joined = member.Joined,
                    Expiration = member.Expiration1,
                    HPhone = member.HPhone,
                    CPhone = member.CPhone2,
                    Address = member.Address?.Replace("Address: ", "").Replace("\r\n", ""),
                    EmailAddress = member.EmailAddress?.Replace("Email: ", ""),
                    CityStateZip = member.CityStateZip,
                    FBIStatus = member.FBIStatus
                };

                context.Members.Add(newMember);
            }
        }

        await context.SaveChangesAsync();
    }
}
