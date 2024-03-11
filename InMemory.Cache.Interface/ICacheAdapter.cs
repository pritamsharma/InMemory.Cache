

namespace InMemory.Cache.Interface
{
    public interface ICacheAdapter
    {
        Task<bool> SetAsync<T>(string key, T value);

        Task<T?> GetAsync<T>(string key);

        Task<bool> IsSetAsync(string key);

        Task<bool> RemoveAsync(string key);
    }
}
