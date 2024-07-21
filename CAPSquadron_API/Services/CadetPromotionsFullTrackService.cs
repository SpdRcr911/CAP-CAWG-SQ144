using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class CadetPromotionsFullTrackService(AppDbContext context) : IRetrieveDataService<CadetPromotionsFullTrack>, IProcessDataService<CadetPromotionsFullTrackCsv>
{
    public async Task<IEnumerable<CadetPromotionsFullTrack>> GetAsync()
    {
        return await context.CadetPromotionsFullTracks.ToListAsync();
    }
    public async Task<CadetPromotionsFullTrack> GetAsync(int id)
    {
        return await context.CadetPromotionsFullTracks.FirstOrDefaultAsync(a => a.Id == id) ?? throw new NotFoundException("Cadet Promotions Full Track not found.");
    }

    public async Task<IEnumerable<CadetPromotionsFullTrack>> GetAsync(IEnumerable<int> ids)
    {
        return await context.CadetPromotionsFullTracks.Where(a => ids.Contains(a.Id)).ToListAsync() ?? throw new NotFoundException("Cadet Promotions Full Track not found.");
    }

    public async Task ProcessAsync(IEnumerable<CadetPromotionsFullTrackCsv> cadetPromotionsFullTrackCsvs)
    {
        foreach (var record in cadetPromotionsFullTrackCsvs)
        {
            var aprDate = TryParseDate(record.AprDate);
            _ = bool.TryParse(record.CadetOath, out var cadetOath);
            _ = bool.TryParse(record.ActivePart, out var activePart);

            var cadetPromotions = await context.CadetPromotionsFullTracks.FirstOrDefaultAsync(a => a.CAPID == record.CAPID && a.AchvName == record.AchvName);

            if (cadetPromotions == null)
            {
                cadetPromotions = new CadetPromotionsFullTrack(record.NameLast ?? string.Empty, record.NameFirst ?? string.Empty, record.Email ?? string.Empty, record.AchvName ?? string.Empty, record.Region ?? string.Empty, record.Wing ?? string.Empty, record.Unit ?? string.Empty)
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
                context.CadetPromotionsFullTracks.Add(cadetPromotions);
            }
            else
            {
                cadetPromotions.NameLast = record.NameLast ?? string.Empty;
                cadetPromotions.NameFirst = record.NameFirst ?? string.Empty;
                cadetPromotions.Email = record.Email ?? string.Empty;
                cadetPromotions.AchvName = record.AchvName ?? string.Empty;
                cadetPromotions.AprDate = aprDate;
                cadetPromotions.JoinDate = TryParseDate(record.JoinDate) ?? default;
                cadetPromotions.Region = record.Region ?? string.Empty;
                cadetPromotions.Wing = record.Wing ?? string.Empty;
                cadetPromotions.Unit = record.Unit ?? string.Empty;
                cadetPromotions.PhyFitTest = TryParseDate(record.PhyFitTest);
                cadetPromotions.LeadLabDateP = TryParseDate(record.LeadLabDateP);
                cadetPromotions.LeadLabScore = record.LeadLabScore;
                cadetPromotions.AEDateP = TryParseDate(record.AEDateP);
                cadetPromotions.AEScore = record.AEScore;
                cadetPromotions.AEModuleOrTest = record.AEModuleOrTest ?? string.Empty;
                cadetPromotions.CharacterDevelopment = TryParseDate(record.CharacterDevelopment);
                cadetPromotions.ActivePart = activePart;
                cadetPromotions.ActiveParticipationDate = TryParseDate(record.ActiveParticipationDate);
                cadetPromotions.CadetOath = cadetOath;
                cadetPromotions.CadetOathDate = TryParseDate(record.CadetOathDate);
                cadetPromotions.LeadershipExpectationsDate = TryParseDate(record.LeadershipExpectationsDate);
                cadetPromotions.UniformDate = TryParseDate(record.UniformDate);
                cadetPromotions.SpecialActivityDate = TryParseDate(record.SpecialActivityDate);
                cadetPromotions.NextApprovalDate = TryParseDate(record.NextApprovalDate);
                cadetPromotions.StaffServiceDate = TryParseDate(record.StaffServiceDate);
                cadetPromotions.OralPresentationDate = TryParseDate(record.OralPresentationDate);
                cadetPromotions.TechnicalWritingAssignmentDate = TryParseDate(record.TechnicalWritingAssignmentDate);
                cadetPromotions.TechnicalWritingAssignment = record.TechnicalWritingAssignment ?? string.Empty;
                cadetPromotions.DrillDate = TryParseDate(record.DrillDate);
                cadetPromotions.DrillScore = record.DrillScore;
                cadetPromotions.WelcomeCourseDate = TryParseDate(record.WelcomeCourseDate);
                cadetPromotions.EssayDate = TryParseDate(record.EssayDate);
                cadetPromotions.SpeechDate = TryParseDate(record.SpeechDate);
                cadetPromotions.AEInteractiveDate = TryParseDate(record.AEInteractiveDate);
                cadetPromotions.AEInteractiveModule = record.AEInteractiveModule ?? string.Empty;
                cadetPromotions.LeadershipInteractiveDate = TryParseDate(record.LeadershipInteractiveDate);
                cadetPromotions.LastModified = DateTime.UtcNow;
                context.CadetPromotionsFullTracks.Update(cadetPromotions);
            }
        }

        await context.SaveChangesAsync();
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