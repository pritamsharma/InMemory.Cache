using InMemory.Cache.Interface;
using Newtonsoft.Json;
using Enyim.Caching;

namespace InMemory.Cache.Memcached
{
    public class MemcachedAdapter : ICacheAdapter
    {

        #region Private Properties 

        /// <summary>
        /// Memcached Service Provider
        /// </summary>
        public IMemcachedClient Client { get; private set; }

        /// <summary>
        /// Cache expiry time
        /// </summary>
        public TimeSpan ExpiryTime { get; private set; }

        /// <summary>
        /// Session Id
        /// </summary>
        private string SessionId { get; set; } = string.Empty;

        /// <summary>
        /// Prefix to be used in addition to user provided Redis Key
        /// </summary>
        private string CacheKeyPrefix { get; set; } = string.Empty;

        #endregion Private Properties

        #region Constructor

        /// <summary>
        /// Adapter for interacting with Redis
        /// </summary>
        public MemcachedAdapter(IMemcachedClient client, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            SetClient(client);
            SetKeys(sessionId, cacheKeyPrefix);
            ExpiryTime = TimeSpan.FromSeconds(expiryTimeSeconds > 0 ? expiryTimeSeconds : 0) ;
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// Creates an connection with Redis database.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void SetClient(IMemcachedClient client) => Client = client ?? throw new ArgumentNullException(nameof(client), "Value can not be null.");

        /// <summary>
        /// Creates Prefix for the key by appending sessionId and and a choosen prefix.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="cacheKeyPrefix"></param>
        private void SetKeys(string sessionId, string cacheKeyPrefix)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                SessionId = string.Empty;
            }
            else
            {
                SessionId = sessionId.Trim();
                CacheKeyPrefix = SessionId + "_";
            }

            CacheKeyPrefix = string.IsNullOrEmpty(cacheKeyPrefix) ? CacheKeyPrefix : CacheKeyPrefix + cacheKeyPrefix.Trim() + "_";
        }

        /// <summary>
        /// Constructs the key be used for Set, Get, IsSet and Remove
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Updated Key</returns>
        private string ConstructKey(string key) => CacheKeyPrefix + key;

        /// <summary>
        /// Serialize object to JSON string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private string SerializeToJson<T>(T value) => value == null ? string.Empty : JsonConvert.SerializeObject(value);

        /// <summary>
        /// Deserialize JSON string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T? DeserializeToObject<T>(string value) => string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value.ToString());

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Sets value specific to the session to the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if successful</returns>
        public async Task<bool> Set<T>(string key, T value)
        {
            var keyValue = ConstructKey(key);

            var serializedValue = SerializeToJson(value);

            return await Client.SetAsync(keyValue, serializedValue, ExpiryTime);
        }

        /// <summary>
        /// Gets value specific to the session from the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T?> Get<T>(string key)
        {
            var keyValue = ConstructKey(key);

            var returnValue = await Client.GetAsync<string>(keyValue);

            return DeserializeToObject<T>(returnValue.Value);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True if successful</returns>
        public async Task<bool> Remove(string key)
        {
            var keyValue = ConstructKey(key);

            return await Client.RemoveAsync(keyValue);
        }

        /// <summary>
        /// Check if a key is present in the cache of current session
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>True if key found</returns>
        public async Task<bool> IsSet(string key)
        {
            var keyValue = ConstructKey(key);

            var value = await Client.GetAsync<string>(keyValue);

            return value.HasValue;
        }

        #endregion Public Methods

    }
}
