namespace CAPSquadron_API.Services;

public interface IXlsxParsingService
{
    IEnumerable<T> ParseXlsx<T>(Stream fileStream, int? rowOffset = null, int? colOffset = null) where T : new();
}