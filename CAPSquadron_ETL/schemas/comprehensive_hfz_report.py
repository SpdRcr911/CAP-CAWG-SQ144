from pydantic import BaseModel
from datetime import date

class ComprehensiveHFZReportBase(BaseModel):
    capid: int
    full_name: str
    org: str
    date_taken: date
    is_passed: str
    weather_waiver: str
    pacer_run: int
    pacer_run_waiver: str
    pacer_run_passed: str
    mile_run: str
    mile_run_waiver: str
    mile_run_passed: str
    curl_up: int
    curl_up_waiver: str
    curl_up_passed: str
    push_up: int
    push_up_waiver: str
    push_up_passed: str
    sit_and_reach: float
    sit_and_reach_waiver: str
    sit_and_reach_passed: str
    first_usr: int
    date_created: date
    usr_id: int
    date_mod: date
    name_last: str
    report_date: date

class ComprehensiveHFZReportCreate(ComprehensiveHFZReportBase):
    pass

class ComprehensiveHFZReport(ComprehensiveHFZReportBase):
    id: int

    class Config:
        from_attributes = True
