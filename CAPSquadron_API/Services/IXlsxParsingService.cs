namespace CAPSquadron_API.Services;

public interface IXlsxParsingService
{
    IEnumerable<T> ParseXlsx<T>(Stream fileStream) where T : new();
}