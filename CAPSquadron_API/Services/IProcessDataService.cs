namespace CAPSquadron_API.Services;

public interface IProcessDataService<T> where T : class
{
    Task ProcessAsync(IEnumerable<T> membersCsv);
}