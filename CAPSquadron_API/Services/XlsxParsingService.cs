using System.Reflection;
using OfficeOpenXml;

namespace CAPSquadron_API.Services;

public class XlsxParsingService : IXlsxParsingService
{
    public IEnumerable<T> ParseXlsx<T>(Stream fileStream, int? rowOffset = null, int? colOffset = null) where T : new()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[0];
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Create a dictionary to map property names to column indexes
        var headerColumns = new Dictionary<string, int>();
        for (var col = (colOffset ?? 1); col <= worksheet.Dimension.Columns; col++)
        {
            var header = worksheet.Cells[(rowOffset ?? 1), col].Text.Trim();
            if (!string.IsNullOrEmpty(header))
            {
                headerColumns[header] = col;
            }
        }

        var items = new List<T>();
        for (var row = ((rowOffset ?? 1) + 1); row <= worksheet.Dimension.Rows; row++)
        {
            var item = new T();
            foreach (var property in properties)
            {
                if (headerColumns.TryGetValue(ColumnName(property), out var col))
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
    private string ColumnName(PropertyInfo? property)
    {
        if (property is null)
            return string.Empty;

        var colAttribute = property.CustomAttributes.FirstOrDefault(p => p.AttributeType.Name == "ColumnAttribute");

        if (colAttribute is null)
            return property.Name;

        if (colAttribute.ConstructorArguments.Count > 0)
        {
            var argumentValue = colAttribute.ConstructorArguments[0].Value;
            if (argumentValue != null)
            {
                return argumentValue.ToString() ?? property.Name;
            }
        }

        return property.Name;
    }
}