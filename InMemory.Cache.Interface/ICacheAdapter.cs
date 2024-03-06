

namespace InMemory.Cache.Interface
{
    public interface ICacheAdapter
    {
        Task<bool> Set<T>(string key, T value);

        Task<T?> Get<T>(string key);

        Task<bool> IsSet(string key);

        Task<bool> Remove(string key);
    }
}
