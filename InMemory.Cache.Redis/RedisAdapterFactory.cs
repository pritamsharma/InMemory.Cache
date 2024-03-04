using InMemory.Cache.Interface;
using StackExchange.Redis;

namespace InMemory.Cache.Redis
{
    public class RedisAdapterFactory : ICacheAdapterFactory
    {
        public ConnectionMultiplexer? Connection { get; private set; }

        private int ExpiryTimeSeconds { get; set; }

        private string Configuration { get; set; }

        private string SessionId { get; set; }

        private string KeyPrefix { get; set; }

        public RedisAdapterFactory(string configuration, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            if (string.IsNullOrEmpty(configuration))
            {
                throw new ArgumentNullException("Configuration value can not be null.");
            }

            Configuration = configuration.Trim();
            ExpiryTimeSeconds = expiryTimeSeconds;
            SessionId = sessionId;
            KeyPrefix = cacheKeyPrefix;
        }

        public ICacheAdapter CreateCacheAdapter()
        {
            Connection = ConnectionMultiplexer.Connect(Configuration);
            var database = Connection.GetDatabase();

            var redisAdapter = new RedisAdapter(database, ExpiryTimeSeconds, SessionId, KeyPrefix);

            return redisAdapter;
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }
    }
}
