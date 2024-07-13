import pandas as pd
from fastapi import UploadFile, HTTPException

def process_file(file: UploadFile):
    if file.filename.endswith('.xlsx'):
        df = pd.read_excel(file.file, header=None, skiprows=7)  # Skip the first 7 rows and read without headers
    elif file.filename.endswith('.csv'):
        df = pd.read_csv(file.file, header=None, skiprows=7)  # Skip the first 7 rows and read without headers
    else:
        raise HTTPException(status_code=400, detail="Invalid file format")

    df = df.dropna(how='all')  # Drop rows where all elements are NaN

    # Define the column indices mapping provided
    column_mapping = {
        0: 'Charter',                      # A
        2: 'Current Cadets',               # C
        4: 'Enrollment (25+ Cadets)',      # E
        5: 'FirstCadets Joined 31 Aug 2023 - 31 Aug 2024',  # F
        7: 'Curry in 8 Wks',               # H
        8: 'Onboarding (70% Curry in 8 Wks)',  # I
        9: 'Cadets w/ WB',                 # J
        10: 'Cadet Achv.(45% w/ WB)',      # K
        11: 'Cadets w/ O\'Flights',        # L
        12: 'O\'Flights (70% w/ First Flight)',  # M
        13: 'Cadets w/Encamp',             # N
        15: 'Encamp (50% w/ Encamp)',      # P
        17: 'Cadets w/ GES',               # R
        18: 'GES (60% w/ GES)',            # S
        19: 'AEX',                         # T
        20: 'STEM',                        # U
        21: 'AE (AEX or STEM kit)',        # V
        22: 'Outside Activities',          # W
        23: 'Seniors w/ TLC',              # X
        24: 'Adult Leadership (3+ TLC Grads)',  # Y
        25: 'Seniors w/ CP Specialty Track Rating',  # Z
        26: 'Specialty Track (2+ Seniors w/Rating)',  # AA
        27: 'QCUA (6+ Criteria Met)',      # AB
        28: 'No. of Criteria Met'          # AC
    }

    # Extract the relevant columns using the indices and rename them
    selected_columns = {v: df.iloc[:, k] for k, v in column_mapping.items()}
    cleaned_df = pd.DataFrame(selected_columns)

    return cleaned_df
