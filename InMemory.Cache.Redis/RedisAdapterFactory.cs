using InMemory.Cache.Interface;
using StackExchange.Redis;

namespace InMemory.Cache.Redis
{
    public class RedisAdapterFactory : ICacheAdapterFactory
    {

        public ConnectionMultiplexer? Connection { get; private set; }

        public IDatabase? Database { get; private set; }

        public int ExpiryTimeSeconds { get; private set; }

        public string Configuration { get; private set; }

        public string SessionId { get; private set; }

        public string KeyPrefix { get; private set; }

        public RedisAdapterFactory(string configuration, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            if (string.IsNullOrEmpty(configuration))
            {
                throw new ArgumentNullException(nameof(configuration), "Value can not be null.");
            }

            Configuration = configuration.Trim();
            ExpiryTimeSeconds = expiryTimeSeconds;
            SessionId = sessionId;
            KeyPrefix = cacheKeyPrefix;
        }

        public ICacheAdapter CreateCacheAdapter()
        {
            Connection = ConnectionMultiplexer.Connect(Configuration);
            Database = Connection.GetDatabase();

            return new RedisAdapter(Database, ExpiryTimeSeconds, SessionId, KeyPrefix);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }
    }
}
