using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;

namespace CAPSquadron_API.Services;

public class XlsxParsingService : IXlsxParsingService
{
    public IEnumerable<T> ParseXlsx<T>(Stream fileStream) where T : new()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[0];
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Create a dictionary to map property names to column indexes
        var headerColumns = new Dictionary<string, int>();
        for (var col = 1; col <= worksheet.Dimension.Columns; col++)
        {
            var header = worksheet.Cells[1, col].Text.Trim();
            if (!string.IsNullOrEmpty(header))
            {
                headerColumns[header] = col;
            }
        }

        var items = new List<T>();
        for (var row = 2; row <= worksheet.Dimension.Rows; row++)
        {
            var item = new T();
            foreach (var property in properties)
            {
                if (headerColumns.TryGetValue(property.Name, out var col))
                {
                    var value = worksheet.Cells[row, col].Text;

                    if (!string.IsNullOrEmpty(value))
                    {
                        var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        var safeValue = (value == null) ? null : Convert.ChangeType(value, propertyType);
                        property.SetValue(item, safeValue, null);
                    }
                }
            }
            items.Add(item);
        }

        return items;
    }
}