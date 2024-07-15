from sqlalchemy.orm import Session
from fastapi import HTTPException, UploadFile
from datetime import datetime
from crud.comprehensive_hfz_report import create_comprehensive_hfz_report, truncate_comprehensive_hfz_report_table
from schemas.comprehensive_hfz_report import ComprehensiveHFZReportCreate
from utils.file_processing import process_file

def process_comprehensive_hfz_report(file: UploadFile):
    df = process_file(file, report_type='comprehensive_hfz_report')
    return df

def save_comprehensive_hfz_report_to_db(df, db: Session, report_date: datetime):
    truncate_comprehensive_hfz_report_table(db)

    for _, row in df.iterrows():
        report_data = ComprehensiveHFZReportCreate(
            capid=row['CAPID'],
            full_name=row['FullName'],
            org=row['Org'],
            date_taken=row['DateTaken'].date() if hasattr(row['DateTaken'], 'date') else row['DateTaken'],
            is_passed=row['IsPassed'],
            weather_waiver=row['WeatherWaiver'],
            pacer_run=row['PacerRun'],
            pacer_run_waiver=row['PacerRunWaiver'],
            pacer_run_passed=row['PacerRunPassed'],
            mile_run=row['MileRun'],
            mile_run_waiver=row['MileRunWaiver'],
            mile_run_passed=row['MileRunPassed'],
            curl_up=row['CurlUp'],
            curl_up_waiver=row['CurlUpWaiver'],
            curl_up_passed=row['CurlUpPassed'],
            push_up=row['PushUp'],
            push_up_waiver=row['PushUpWaiver'],
            push_up_passed=row['PushUpPassed'],
            sit_and_reach=row['SitAndReach'],
            sit_and_reach_waiver=row['SitAndReachWaiver'],
            sit_and_reach_passed=row['SitAndReachPassed'],
            first_usr=row['FirstUsr'],
            date_created=row['DateCreated'].date() if hasattr(row['DateCreated'], 'date') else row['DateCreated'],
            usr_id=row['UsrID'],
            date_mod=row['DateMod'].date() if hasattr(row['DateMod'], 'date') else row['DateMod'],
            name_last=row['NameLast'],
            report_date=report_date
        )
        create_comprehensive_hfz_report(db, report_data)