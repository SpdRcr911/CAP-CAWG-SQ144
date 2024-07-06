namespace CAPSquadron_API.Services;


public interface IRetrieveDataService<T> where T : class
{
    Task<List<T>> GetAsync();
}
