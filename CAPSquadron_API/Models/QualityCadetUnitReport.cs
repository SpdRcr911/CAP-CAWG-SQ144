using System.ComponentModel.DataAnnotations.Schema;

namespace CAPSquadron_API.Models;

public class QualityCadetUnitReport
{
    public int Id { get; set; }
    public string? Charter { get; set; }
    public int CurrentCadets { get; set; }

    [Column("enrollment_25_plus_cadets")]
    public string? Enrollment25PlusCadets { get; set; }

    public int CadetsJoined { get; set; }

    [Column("curry_in_8_weeks")]
    public int CurryIn8Weeks { get; set; }

    [Column("onboarding_70_percent")]
    public string? Onboarding70Percent { get; set; }

    [Column("cadets_with_wb")]
    public int CadetsWithWB { get; set; }

    [Column("cadet_achievement_45_percent")]
    public string? CadetAchievement45Percent { get; set; }

    [Column("cadets_with_oflights")]
    public int CadetsWithOFlights { get; set; }

    [Column("oflights_70_percent")]
    public string? OFlights70Percent { get; set; }

    [Column("cadets_with_encamp")]
    public int CadetsWithEncamp { get; set; }

    [Column("encamp_50_percent")]
    public string? Encamp50Percent { get; set; }

    [Column("cadets_with_ges")]
    public int CadetsWithGES { get; set; }

    [Column("ges_60_percent")]
    public string? GES60Percent { get; set; }

    public string? AEX { get; set; }
    public string? STEM { get; set; }

    [Column("ae_aex_or_stem_kit")]
    public string? AEAexOrStemKit { get; set; }

    [Column("outside_activities")]
    public string? OutsideActivities { get; set; }

    [Column("seniors_with_tlc")]
    public int SeniorsWithTLC { get; set; }

    [Column("adult_leadership_3_plus_tlc_grads")]
    public string? AdultLeadership3PlusTlcGrads { get; set; }

    [Column("seniors_with_cp_specialty_track_rating")]
    public int SeniorsWithCPSpecialtyTrackRating { get; set; }

    [Column("specialty_track_2_plus_seniors_with_rating")]
    public string? SpecialtyTrack2PlusSeniorsWithRating { get; set; }

    [Column("qcua_6_plus_criteria_met")]
    public string? QCUA6PlusCriteriaMet { get; set; }

    [Column("number_of_criteria_met")]
    public int NumberOfCriteriaMet { get; set; }

    [Column("report_date")]
    public DateTime ReportDate { get; set; }
}
