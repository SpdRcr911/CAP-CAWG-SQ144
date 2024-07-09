using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CAPSquadron_API.Services;

public class AchievementService(AppDbContext context) : IRetrieveDataService<Achievement>, IProcessDataService<AchievementCsvModel>
{
    private readonly AppDbContext _context = context;
    public async Task<IEnumerable<Achievement>> GetAsync()
    {
        return await _context.Achievements.ToListAsync();
    }
    public async Task<Achievement> GetAsync(int id)
    {
        return await _context.Achievements.FirstOrDefaultAsync(a => a.Id == id) ?? throw new NotFoundException("Achievement not found.");
    }

    public async Task<IEnumerable<Achievement>> GetAsync(IEnumerable<int> ids)
    {
        return await _context.Achievements.Where(a => ids.Contains(a.Id)).ToListAsync() ?? throw new NotFoundException("Achievement not found.");
    }

    public async Task ProcessAsync(IEnumerable<AchievementCsvModel> achievementsCsv)
    {
        foreach (var record in achievementsCsv)
        {
            var aprDate = TryParseDate(record.AprDate);
            _ = bool.TryParse(record.CadetOath, out var cadetOath);
            _ = bool.TryParse(record.ActivePart, out var activePart);

            var achievement = await _context.Achievements.FirstOrDefaultAsync(a => a.CAPID == record.CAPID && a.AchvName == record.AchvName);

            if (achievement == null)
            {
                achievement = new Achievement(record.NameLast ?? string.Empty, record.NameFirst ?? string.Empty, record.Email ?? string.Empty, record.AchvName ?? string.Empty, record.Region ?? string.Empty, record.Wing ?? string.Empty, record.Unit ?? string.Empty)
                {
                    CAPID = record.CAPID,
                    AprDate = aprDate,
                    JoinDate = DateOnly.Parse(record.JoinDate!),
                    PhyFitTest = TryParseDate(record.PhyFitTest),
                    LeadLabDateP = TryParseDate(record.LeadLabDateP),
                    LeadLabScore = record.LeadLabScore,
                    AEDateP = TryParseDate(record.AEDateP),
                    AEScore = record.AEScore,
                    AEModuleOrTest = record.AEModuleOrTest ?? string.Empty,
                    CharacterDevelopment = TryParseDate(record.CharacterDevelopment),
                    ActivePart = activePart,
                    ActiveParticipationDate = TryParseDate(record.ActiveParticipationDate),
                    CadetOath = cadetOath,
                    CadetOathDate = TryParseDate(record.CadetOathDate),
                    LeadershipExpectationsDate = TryParseDate(record.LeadershipExpectationsDate),
                    UniformDate = TryParseDate(record.UniformDate),
                    SpecialActivityDate = TryParseDate(record.SpecialActivityDate),
                    NextApprovalDate = TryParseDate(record.NextApprovalDate),
                    StaffServiceDate = TryParseDate(record.StaffServiceDate),
                    OralPresentationDate = TryParseDate(record.OralPresentationDate),
                    TechnicalWritingAssignmentDate = TryParseDate(record.TechnicalWritingAssignmentDate),
                    TechnicalWritingAssignment = record.TechnicalWritingAssignment ?? string.Empty,
                    DrillDate = TryParseDate(record.DrillDate),
                    DrillScore = record.DrillScore,
                    WelcomeCourseDate = TryParseDate(record.WelcomeCourseDate),
                    EssayDate = TryParseDate(record.EssayDate),
                    SpeechDate = TryParseDate(record.SpeechDate),
                    AEInteractiveDate = TryParseDate(record.AEInteractiveDate),
                    AEInteractiveModule = record.AEInteractiveModule ?? string.Empty,
                    LeadershipInteractiveDate = TryParseDate(record.LeadershipInteractiveDate),
                    LastModified = DateTime.UtcNow
                };
                _context.Achievements.Add(achievement);
            }
            else
            {
                achievement.NameLast = record.NameLast ?? string.Empty;
                achievement.NameFirst = record.NameFirst ?? string.Empty;
                achievement.Email = record.Email ?? string.Empty;
                achievement.AchvName = record.AchvName ?? string.Empty;
                achievement.AprDate = aprDate;
                achievement.JoinDate = TryParseDate(record.JoinDate) ?? default;
                achievement.Region = record.Region ?? string.Empty;
                achievement.Wing = record.Wing ?? string.Empty;
                achievement.Unit = record.Unit ?? string.Empty;
                achievement.PhyFitTest = TryParseDate(record.PhyFitTest);
                achievement.LeadLabDateP = TryParseDate(record.LeadLabDateP);
                achievement.LeadLabScore = record.LeadLabScore;
                achievement.AEDateP = TryParseDate(record.AEDateP);
                achievement.AEScore = record.AEScore;
                achievement.AEModuleOrTest = record.AEModuleOrTest ?? string.Empty;
                achievement.CharacterDevelopment = TryParseDate(record.CharacterDevelopment);
                achievement.ActivePart = activePart;
                achievement.ActiveParticipationDate = TryParseDate(record.ActiveParticipationDate);
                achievement.CadetOath = cadetOath;
                achievement.CadetOathDate = TryParseDate(record.CadetOathDate);
                achievement.LeadershipExpectationsDate = TryParseDate(record.LeadershipExpectationsDate);
                achievement.UniformDate = TryParseDate(record.UniformDate);
                achievement.SpecialActivityDate = TryParseDate(record.SpecialActivityDate);
                achievement.NextApprovalDate = TryParseDate(record.NextApprovalDate);
                achievement.StaffServiceDate = TryParseDate(record.StaffServiceDate);
                achievement.OralPresentationDate = TryParseDate(record.OralPresentationDate);
                achievement.TechnicalWritingAssignmentDate = TryParseDate(record.TechnicalWritingAssignmentDate);
                achievement.TechnicalWritingAssignment = record.TechnicalWritingAssignment ?? string.Empty;
                achievement.DrillDate = TryParseDate(record.DrillDate);
                achievement.DrillScore = record.DrillScore;
                achievement.WelcomeCourseDate = TryParseDate(record.WelcomeCourseDate);
                achievement.EssayDate = TryParseDate(record.EssayDate);
                achievement.SpeechDate = TryParseDate(record.SpeechDate);
                achievement.AEInteractiveDate = TryParseDate(record.AEInteractiveDate);
                achievement.AEInteractiveModule = record.AEInteractiveModule ?? string.Empty;
                achievement.LeadershipInteractiveDate = TryParseDate(record.LeadershipInteractiveDate);
                achievement.LastModified = DateTime.UtcNow;
                _context.Achievements.Update(achievement);
            }
        }

        await _context.SaveChangesAsync();
    }
    private static DateOnly? TryParseDate(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString) || dateString.Equals("None", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (DateOnly.TryParse(dateString, out var date))
        {
            return date;
        }

        return null;
    }

}