using CAPSquadron_API.Data;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using CAPSquadron_API.Exceptions;
using OfficeOpenXml;
using System.Globalization;

namespace CAPSquadron_API.Services;

public interface ICadetPhysicalFitnessTrainingReportService
{
    Task<byte[]> GetFitnessWorksheetsAsync();
}
internal class CadetPhysicalFitnessTrainingReportService(AppDbContext context) : IRetrieveDataService<CadetPhysicalFitnessTrainingReport>, IProcessDataService<CadetPhysicalFitnessTrainingReportCsv>, ICadetPhysicalFitnessTrainingReportService
{
    public async Task<IEnumerable<CadetPhysicalFitnessTrainingReport>> GetAsync()
    {
        return await context.CadetPhysicalFitnessTrainingReports.ToListAsync();
    }

    public async Task<CadetPhysicalFitnessTrainingReport> GetAsync(int id)
    {
        return await context.CadetPhysicalFitnessTrainingReports.FirstOrDefaultAsync(c => c.CAPID == id) ?? throw new NotFoundException("Cadet Physical Fitness Training Report");
    }

    public async Task<IEnumerable<CadetPhysicalFitnessTrainingReport>> GetAsync(IEnumerable<int> ids)
    {
        return await context.CadetPhysicalFitnessTrainingReports.Where(c => ids.Contains(c.CAPID)).ToListAsync();
    }

    public async Task ProcessAsync(IEnumerable<CadetPhysicalFitnessTrainingReportCsv> cpftrCvs)
    {
        context.TruncateCadetPhysicalFitnessTrainingReports();
        var timeStamp = DateTimeOffset.UtcNow;

        foreach (var report in cpftrCvs)
        {
            if (report.FullName is null || report.FullName == string.Empty) continue;
            var fullNameMatch = Regex.Match(report.FullName!, "(C/.+?|CADET)\\s(.+?)\\s(\\d+)");
            var newCpftr = new CadetPhysicalFitnessTrainingReport()
            {
                CAPID = int.Parse(fullNameMatch.Groups[3].Value),
                FullName = fullNameMatch.Groups[2].Value,
                Rank = fullNameMatch.Groups[1].Value,
                HFZCred = report.HFZCred,
                CurlUpReq = report.CurlUpReq,
                PushUpReq = report.PushUpReq,
                MileRunReq = TimeSpan.Parse($"00:{report.MileRunReq!}"),
                PacerRunReq = report.PacerRunReq,
                SitAndReachReq = report.SitAndReachReq,
                Expiration = DateOnly.TryParse(report.Expiration, out var expiration) ? expiration : null,
                RecordTimeStamp = timeStamp
            };
            context.CadetPhysicalFitnessTrainingReports.Add(newCpftr);
        }
        await context.SaveChangesAsync();
    }

    public async Task<byte[]> GetFitnessWorksheetsAsync()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var cadetReports = await context.CadetPhysicalFitnessTrainingReports.ToListAsync();
        var flights = await context.Flights.ToListAsync();
        var flightMembership = await context.FlightMembers.ToListAsync();

        using var package = new ExcelPackage();

        foreach (var flight in flights)
        {
            var worksheet = package.Workbook.Worksheets.Add(flight.Name);

            // Add the title row
            AddTitleRow(worksheet, flight.Name);

            // Set headers
            SetWorksheetHeaders(worksheet);
            SetColumnWidths(worksheet);

            // Freeze panes after the title row
            worksheet.View.FreezePanes(3, 1);

            var flightMembers = flightMembership
                .Where(fm => fm.FlightId == flight.Id)
                .Select(fm => fm.CAPID)
                .ToList();

            var flightReports = cadetReports
                .Where(cr => flightMembers.Contains(cr.CAPID))
                .ToList();

            AddReportDataToWorksheet(worksheet, flightReports);

            // AutoFit the first column (Full Name)
            worksheet.Column(1).AutoFit();

            // Set print area
            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, flightReports.Count + 2, 10];

            // Set print options
            SetPrintOptions(worksheet);

            // Add borders to data cells
            AddBordersToCells(worksheet, flightReports.Count);

        }

        return package.GetAsByteArray();
    }

    private static void AddTitleRow(ExcelWorksheet worksheet, string flightName)
    {
        var title = $"{flightName} - {DateTime.Now:MMMM yyyy}";
        worksheet.Cells["A1:J1"].Merge = true;
        worksheet.Cells["A1:J1"].Value = title;
        worksheet.Cells["A1:J1"].Style.Font.Size = 24;
        worksheet.Cells["A1:J1"].Style.Font.Bold = true;
        worksheet.Cells["A1:J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        worksheet.Cells["A1:J1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        worksheet.Row(1).Height = 30;
    }

    private static void SetWorksheetHeaders(ExcelWorksheet worksheet)
    {
        string[] headers = { "Full Name", "Mile Run Req.", "Mile Run Score", "Curl Up Req.",
                         "Curl Up Score", "Push Up Req.", "Push Up Score", "Sit & Reach Req.",
                         "Sit & Reach Score", "Expiration" };

        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cells[2, i + 1].Value = headers[i];
        }

        using var range = worksheet.Cells["A2:J2"];
        range.Style.Font.Bold = true;
        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        worksheet.Row(2).Height = 23; // Explicitly set the row height for the header row
    }

    private static void SetColumnWidths(ExcelWorksheet worksheet)
    {
        double[] columnWidths = { 20, 13, 15, 13, 15, 12, 15, 15, 15, 13 };
        for (int col = 1; col <= columnWidths.Length; col++)
        {
            worksheet.Column(col).Width = columnWidths[col - 1];
        }
    }

    private static void AddReportDataToWorksheet(ExcelWorksheet worksheet, List<CadetPhysicalFitnessTrainingReport> flightReports)
    {
        for (int i = 0; i < flightReports.Count; i++)
        {
            var report = flightReports[i];
            var row = i + 3;

            worksheet.Cells[row, 1].Value = report.FullName;

            worksheet.Cells[row, 2].Value = report.MileRunReq;
            worksheet.Cells[row, 2].Style.Numberformat.Format = "mm:ss"; // Format as mm:ss

            worksheet.Cells[row, 3].Value = string.Empty; // Mile Run Score
            worksheet.Cells[row, 4].Value = report.CurlUpReq;
            worksheet.Cells[row, 5].Value = string.Empty; // Curl Up Score
            worksheet.Cells[row, 6].Value = report.PushUpReq;
            worksheet.Cells[row, 7].Value = string.Empty; // Push Up Score
            worksheet.Cells[row, 8].Value = report.SitAndReachReq;
            worksheet.Cells[row, 9].Value = string.Empty; // Sit & Reach Score
            if (report.Expiration.HasValue)
            {
                worksheet.Cells[row, 10].Value = report.Expiration.Value;
                worksheet.Cells[row, 10].Style.Numberformat.Format = "dd MMM yy"; // Format as dd MMM yy
            }

            // Set font size to 14 for all cells with values
            for (int col = 1; col <= 10; col++)
            {
                var cell = worksheet.Cells[row, col];
                if (!string.IsNullOrEmpty(cell.Text))
                {
                    cell.Style.Font.Size = 14;
                }
            }

            // Center alignment for specific columns
            foreach (int col in new[] { 2, 4, 5, 6, 8, 10 })
            {
                worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
        }
    }

    private static void SetPrintOptions(ExcelWorksheet worksheet)
    {
        worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
        worksheet.PrinterSettings.FitToPage = true;
        worksheet.PrinterSettings.FitToWidth = 1; // Fit to one page wide
        worksheet.PrinterSettings.FitToHeight = 0; // Fit to as many pages tall as needed
        worksheet.PrinterSettings.Scale = 100; // Default scaling

        worksheet.PrinterSettings.TopMargin = 0.5m;
        worksheet.PrinterSettings.BottomMargin = 0.5m;
        worksheet.PrinterSettings.LeftMargin = 0.5m;
        worksheet.PrinterSettings.RightMargin = 0.5m;
    }

    private static void AddBordersToCells(ExcelWorksheet worksheet, int rowCount)
    {
        using var range = worksheet.Cells[1, 1, rowCount + 2, 10];
        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
    }


}
