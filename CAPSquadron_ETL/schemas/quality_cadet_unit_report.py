from pydantic import BaseModel
from datetime import date

class QualityCadetUnitReportBase(BaseModel):
    charter: str
    current_cadets: int
    enrollment_25_plus_cadets: str
    cadets_joined: int
    curry_in_8_weeks: int
    onboarding_70_percent: str
    cadets_with_wb: int
    cadet_achievement_45_percent: str
    cadets_with_oflights: int
    oflights_70_percent: str
    cadets_with_encamp: int
    encamp_50_percent: str
    cadets_with_ges: int
    ges_60_percent: str
    aex: str
    stem: str
    ae_aex_or_stem_kit: str
    outside_activities: str
    seniors_with_tlc: int
    adult_leadership_3_plus_tlc_grads: str
    seniors_with_cp_specialty_track_rating: int
    specialty_track_2_plus_seniors_with_rating: str
    qcua_6_plus_criteria_met: str
    number_of_criteria_met: int
    report_date: date

class QualityCadetUnitReportCreate(QualityCadetUnitReportBase):
    pass

class QualityCadetUnitReport(QualityCadetUnitReportBase):
    id: int

    class Config:
        orm_mode = True
