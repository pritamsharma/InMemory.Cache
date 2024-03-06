using InMemory.Cache.Interface;
using Microsoft.Extensions.DependencyInjection;
using Enyim.Caching.Configuration;
using Enyim.Caching;
using Microsoft.Extensions.Configuration;

namespace InMemory.Cache.Memcached
{
    public class MemcachedAdapterFactory : ICacheAdapterFactory
    {

        public IMemcachedClient? MemcachedClient { get; private set; }

        public int ExpiryTimeSeconds { get; private set; }

        public string Address { get; private set; }

        public int Port { get; private set; }

        public string SessionId { get; private set; }

        public string KeyPrefix { get; private set; }

        public MemcachedAdapterFactory(string address, int port, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            if (string.IsNullOrEmpty(address) || port <= 0)
            {
                throw new ArgumentNullException(nameof(address), "Value can not be null.");
            }

            Address = address;
            Port = port;
            ExpiryTimeSeconds = expiryTimeSeconds;
            SessionId = sessionId;
            KeyPrefix = cacheKeyPrefix;
        }

        public ICacheAdapter CreateCacheAdapter()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddEnyimMemcached(o => o.Servers = new List<Server> { new() { Address = Address, Port = Port } });
            var provider = services.BuildServiceProvider();

            MemcachedClient = provider.GetService<IMemcachedClient>();

            var memcachedAdapter = new MemcachedAdapter(MemcachedClient, ExpiryTimeSeconds, SessionId, KeyPrefix);

            return memcachedAdapter;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && MemcachedClient != null)
            {
                MemcachedClient.Dispose();
            }
        }

    }
}
