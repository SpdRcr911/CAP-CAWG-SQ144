using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class AttendanceSignInService(AppDbContext context) : IRetrieveDataService<AttendanceSignIn>, IProcessDataService<AttendanceSignInCsvModel>
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<AttendanceSignIn>> GetAsync()
    {
        return await _context.AttendanceSignIns.ToListAsync();
    }

    public async Task<AttendanceSignIn> GetAsync(int capid)
    {
        return await _context.AttendanceSignIns.FirstOrDefaultAsync(m => m.CAPID == capid) ?? throw new NotFoundException("AttendanceSignIn not found.");
    }

    public async Task<IEnumerable<AttendanceSignIn>> GetAsync(IEnumerable<int> capid)
    {
        return await _context.AttendanceSignIns.Where(m => capid.Contains(m.CAPID)).ToListAsync() ?? throw new NotFoundException("AttendanceSignIn not found.");
    }

    public async Task ProcessAsync(IEnumerable<AttendanceSignInCsvModel> attendanceSignInCsv)
    {
        var capIdsInCsv = attendanceSignInCsv.Select(r => r.CAPID).ToHashSet();
        var attendanceSignIn = await _context.AttendanceSignIns.ToListAsync();
        var currentTime = DateTime.UtcNow;

        // Prepare lists for batch processing
        var attendanceSignInToAdd = new List<AttendanceSignIn>();
        var attendanceSignInToUpdate = new List<AttendanceSignIn>();

        // Mark all attendanceSignIn not in CSV as inactive
        foreach (var attendanceData in attendanceSignIn)
        {
            if (!capIdsInCsv.Contains(attendanceData.CAPID) && attendanceData.InactiveDate == null)
            {
                attendanceData.InactiveDate = currentTime;
                attendanceData.LastModified = currentTime;
                attendanceSignInToUpdate.Add(attendanceData);
            }
        }

        foreach (var record in attendanceSignInCsv)
        {
            var (Name, EoCompleted, OpsecCompleted, SafetyCurrent) = ParseMemberInfo(record.Member);
            var member = attendanceSignIn.FirstOrDefault(m => m.CAPID == record.CAPID);

            if (member == null)
            {
                member = new AttendanceSignIn(Name, record.Rank)
                {
                    CAPID = record.CAPID,
                    Expiration = record.Expiration,
                    EoCompleted = EoCompleted,
                    OpsecCompleted = OpsecCompleted,
                    SafetyCurrent = SafetyCurrent,
                    LastModified = currentTime
                };
                attendanceSignInToAdd.Add(member);
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
                attendanceSignInToUpdate.Add(member);
            }
        }

        // Batch processing
        if (attendanceSignInToAdd.Any())
        {
            await _context.AttendanceSignIns.AddRangeAsync(attendanceSignInToAdd);
        }

        if (attendanceSignInToUpdate.Any())
        {
            _context.AttendanceSignIns.UpdateRange(attendanceSignInToUpdate);
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