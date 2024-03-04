using InMemory.Cache.Interface;
using Microsoft.Extensions.DependencyInjection;
using Enyim.Caching.Configuration;
using Enyim.Caching;

namespace InMemory.Cache.Memcached
{
    public class MemcachedAdapterFactory : ICacheAdapterFactory
    {

        private IMemcachedClient MemcachedClient { get; set; }

        private int ExpiryTimeSeconds { get; set; }

        private string Address { get; set; }

        private int Port { get; set; }

        private string SessionId { get; set; }

        private string KeyPrefix { get; set; }

        public MemcachedAdapterFactory(string address, int port, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            if (string.IsNullOrEmpty(address) || port <= 0)
            {
                throw new ArgumentNullException("Address or Port value can not be null.");
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
            services.AddEnyimMemcached(o => o.Servers = new List<Server> { new Server { Address = Address, Port = Port } });
            var provider = services.BuildServiceProvider();

            MemcachedClient = provider.GetService<IMemcachedClient>();

            var memcachedAdapter = new MemcachedAdapter(MemcachedClient, ExpiryTimeSeconds, SessionId, KeyPrefix);

            return memcachedAdapter;
        }

        public void Dispose()
        {
            if (MemcachedClient != null)
            {
                MemcachedClient.Dispose();
            }
        }
    }
}
