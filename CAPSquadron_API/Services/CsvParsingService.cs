using CAPSquadron_API.Models;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace CAPSquadron_API.Services;

public class CsvParsingService : ICsvParsingService
{
    public IEnumerable<T> ParseCsv<T>(Stream fileStream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            MissingFieldFound = null
        };

        using var stream = new StreamReader(fileStream);
        using var csv = new CsvReader(stream, config);
        return csv.GetRecords<T>().ToList();
    }
}
