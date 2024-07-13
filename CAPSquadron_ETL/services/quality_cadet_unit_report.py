# services/quality_cadet_unit_report.py

from sqlalchemy.orm import Session
from fastapi import HTTPException, UploadFile
from datetime import datetime
from crud.quality_cadet_unit_report import create_quality_cadet_unit_report
from schemas.quality_cadet_unit_report import QualityCadetUnitReportCreate
from utils.file_processing import process_file

def process_quality_cadet_unit_report(file: UploadFile):
    df = process_file(file)
    return df

def save_quality_cadet_unit_report_to_db(df, db: Session, report_date: datetime):
    for _, row in df.iterrows():
        report_data = QualityCadetUnitReportCreate(
            charter=row['Charter'],
            current_cadets=row['Current Cadets'],
            enrollment_25_plus_cadets=row['Enrollment (25+ Cadets)'],
            cadets_joined=row['FirstCadets Joined 31 Aug 2023 - 31 Aug 2024'],
            curry_in_8_weeks=row['Curry in 8 Wks'],
            onboarding_70_percent=row['Onboarding (70% Curry in 8 Wks)'],
            cadets_with_wb=row['Cadets w/ WB'],
            cadet_achievement_45_percent=row['Cadet Achv.(45% w/ WB)'],
            cadets_with_oflights=row['Cadets w/ O\'Flights'],
            oflights_70_percent=row['O\'Flights (70% w/ First Flight)'],
            cadets_with_encamp=row['Cadets w/Encamp'],
            encamp_50_percent=row['Encamp (50% w/ Encamp)'],
            cadets_with_ges=row['Cadets w/ GES'],
            ges_60_percent=row['GES (60% w/ GES)'],
            aex=row['AEX'],
            stem=row['STEM'],
            ae_aex_or_stem_kit=row['AE (AEX or STEM kit)'],
            outside_activities=row['Outside Activities'],
            seniors_with_tlc=row['Seniors w/ TLC'],
            adult_leadership_3_plus_tlc_grads=row['Adult Leadership (3+ TLC Grads)'],
            seniors_with_cp_specialty_track_rating=row['Seniors w/ CP Specialty Track Rating'],
            specialty_track_2_plus_seniors_with_rating=row['Specialty Track (2+ Seniors w/Rating)'],
            qcua_6_plus_criteria_met=row['QCUA (6+ Criteria Met)'],
            number_of_criteria_met=row['No. of Criteria Met'],
            report_date=report_date
        )
        create_quality_cadet_unit_report(db, report_data)
