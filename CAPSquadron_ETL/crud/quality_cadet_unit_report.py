from sqlalchemy.orm import Session
from models import QualityCadetUnitReport
from schemas.quality_cadet_unit_report import QualityCadetUnitReportCreate

def create_quality_cadet_unit_report(db: Session, report: QualityCadetUnitReportCreate):
    db_report = QualityCadetUnitReport(**report.dict())
    db.add(db_report)
    db.commit()
    db.refresh(db_report)
    return db_report
