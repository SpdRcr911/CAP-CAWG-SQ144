from sqlalchemy.orm import Session
from sqlalchemy import text
from models import ComprehensiveHFZReport
from schemas.comprehensive_hfz_report import ComprehensiveHFZReportCreate

def create_comprehensive_hfz_report(db: Session, report: ComprehensiveHFZReportCreate):
    db_report = ComprehensiveHFZReport(**report.dict())
    db.add(db_report)
    db.commit()
    db.refresh(db_report)
    return db_report

def truncate_comprehensive_hfz_report_table(db: Session):
    db.execute(text("TRUNCATE TABLE comprehensive_hfz_reports RESTART IDENTITY CASCADE;"))
    db.commit()
