import pandas as pd
from fastapi import UploadFile, HTTPException

def process_file(file: UploadFile, report_type: str, encoding='utf-8'):
    try:
        if file.filename.endswith('.xlsx') or file.filename.endswith('.csv'):
            if report_type == 'quality_cadet_unit_report':
                df = pd.read_csv(file.file, header=None, skiprows=7)  # Skip the first 7 rows and read without headers
            else:
                df = pd.read_excel(file.file) 
        else:
            raise HTTPException(status_code=400, detail="Invalid file format")
    except UnicodeDecodeError as e:
        raise HTTPException(status_code=400, detail=f"File encoding error: {e}")


    df = df.dropna(how='all')  # Drop rows where all elements are NaN

    if report_type == 'comprehensive_hfz_report':
        column_mapping = {
            0: 'CAPID',
            1: 'FullName',
            2: 'Org',
            3: 'DateTaken',
            4: 'IsPassed',
            5: 'WeatherWaiver',
            6: 'PacerRun',
            7: 'PacerRunWaiver',
            8: 'PacerRunPassed',
            9: 'MileRun',
            10: 'MileRunWaiver',
            11: 'MileRunPassed',
            12: 'CurlUp',
            13: 'CurlUpWaiver',
            14: 'CurlUpPassed',
            15: 'PushUp',
            16: 'PushUpWaiver',
            17: 'PushUpPassed',
            18: 'SitAndReach',
            19: 'SitAndReachWaiver',
            20: 'SitAndReachPassed',
            21: 'FirstUsr',
            22: 'DateCreated',
            23: 'UsrID',
            24: 'DateMod',
            25: 'NameLast',
        }
        selected_columns = {v: df.iloc[:, k] for k, v in column_mapping.items()}
        cleaned_df = pd.DataFrame(selected_columns)
    else:
        cleaned_df = df  # Use the dataframe as is for 'quality_cadet_unit_report'

    return cleaned_df
