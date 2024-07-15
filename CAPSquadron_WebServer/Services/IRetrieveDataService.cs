namespace CAPSquadron_WebServer.Services;

public interface IRetrieveDataService<T> where T : class
{
    Task<IEnumerable<T>> GetAsync();
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAsync(IEnumerable<int> ids);
}
