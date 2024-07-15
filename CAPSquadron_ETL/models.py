from sqlalchemy import Column, Integer, String, Float, Date
from database import Base

class QualityCadetUnitReport(Base):
    __tablename__ = "quality_cadet_unit_reports"
    
    id = Column(Integer, primary_key=True, index=True)
    charter = Column(String)
    current_cadets = Column(Integer)
    enrollment_25_plus_cadets = Column(String)
    cadets_joined = Column(Integer)
    curry_in_8_weeks = Column(Integer)
    onboarding_70_percent = Column(String)
    cadets_with_wb = Column(Integer)
    cadet_achievement_45_percent = Column(String)
    cadets_with_oflights = Column(Integer)
    oflights_70_percent = Column(String)
    cadets_with_encamp = Column(Integer)
    encamp_50_percent = Column(String)
    cadets_with_ges = Column(Integer)
    ges_60_percent = Column(String)
    aex = Column(String)
    stem = Column(String)
    ae_aex_or_stem_kit = Column(String)
    outside_activities = Column(String)
    seniors_with_tlc = Column(Integer)
    adult_leadership_3_plus_tlc_grads = Column(String)
    seniors_with_cp_specialty_track_rating = Column(Integer)
    specialty_track_2_plus_seniors_with_rating = Column(String)
    qcua_6_plus_criteria_met = Column(String)
    number_of_criteria_met = Column(Integer)
    report_date = Column(Date, index=True)

class ComprehensiveHFZReport(Base):
    __tablename__ = "comprehensive_hfz_reports"
    
    id = Column(Integer, primary_key=True, index=True)
    capid = Column(Integer, index=True)
    full_name = Column(String)
    org = Column(String)
    date_taken = Column(Date)
    is_passed = Column(String)
    weather_waiver = Column(String)
    pacer_run = Column(Integer)
    pacer_run_waiver = Column(String)
    pacer_run_passed = Column(String)
    mile_run = Column(String)
    mile_run_waiver = Column(String)
    mile_run_passed = Column(String)
    curl_up = Column(Integer)
    curl_up_waiver = Column(String)
    curl_up_passed = Column(String)
    push_up = Column(Integer)
    push_up_waiver = Column(String)
    push_up_passed = Column(String)
    sit_and_reach = Column(Float)
    sit_and_reach_waiver = Column(String)
    sit_and_reach_passed = Column(String)
    first_usr = Column(Integer)
    date_created = Column(Date)
    usr_id = Column(Integer)
    date_mod = Column(Date)
    name_last = Column(String)
    report_date = Column(Date, index=True)

# Create the tables in the database
from database import engine

Base.metadata.create_all(bind=engine)
