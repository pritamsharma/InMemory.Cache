
namespace InMemory.Cache.Interface
{
    public interface ICacheAdapterFactory: IDisposable
    {
        ICacheAdapter CreateCacheAdapter();
    }
}
