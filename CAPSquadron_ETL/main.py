from fastapi import FastAPI, File, UploadFile, Depends, HTTPException, Form
from sqlalchemy.orm import Session
from database import get_db
from services.quality_cadet_unit_report import process_quality_cadet_unit_report, save_quality_cadet_unit_report_to_db
from datetime import datetime

app = FastAPI()

@app.post("/upload/quality_cadet_unit_report/")
async def upload_quality_cadet_unit_report(
        file: UploadFile = File(...),
        report_date: str = Form(...),
        db: Session = Depends(get_db)):
    try:
        report_date = datetime.strptime(report_date, "%Y-%m-%d")
    except ValueError:
        raise HTTPException(status_code=400, detail="Invalid date format, expected YYYY-MM-DD")
    
    df = process_quality_cadet_unit_report(file)
    save_quality_cadet_unit_report_to_db(df, db, report_date)
    return {"status": "success", "filename": file.filename, "report_type": "quality_cadet_unit_report"}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
