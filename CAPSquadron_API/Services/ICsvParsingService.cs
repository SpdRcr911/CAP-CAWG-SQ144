using CAPSquadron_API.Models;

namespace CAPSquadron_API.Services;

public interface ICsvParsingService
{
    IEnumerable<T> ParseCsv<T>(Stream fileStream);
}
