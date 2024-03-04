

namespace InMemory.Cache.Interface
{
    public interface ICacheAdapter
    {
        bool Set<T>(string key, T value);

        T? Get<T>(string key);

        bool IsSet(string key);

        bool Remove(string key);
    }
}
