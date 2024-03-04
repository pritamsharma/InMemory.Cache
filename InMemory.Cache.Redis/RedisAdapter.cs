﻿using StackExchange.Redis;
using Newtonsoft.Json;
using InMemory.Cache.Interface;

namespace InMemory.Cache.Redis
{

    /// <summary>
    /// Adapter for interacting with Redis
    /// </summary>
    public class RedisAdapter : ICacheAdapter
    {
        #region Private Properties 

        /// <summary>
        /// Redis database
        /// </summary>
        public IDatabase Database { get; private set; }

        /// <summary>
        /// Cache expiry time
        /// </summary>
        public TimeSpan? ExpiryTime { get; private set; }

        /// <summary>
        /// Session Id
        /// </summary>
        private string SessionId { get; set; }

        /// <summary>
        /// Prefix to be used in addition to user provided Redis Key
        /// </summary>
        private string CacheKeyPrefix { get; set; }

        #endregion Private Properties

        #region Constructor

        /// <summary>
        /// Adapter for interacting with Redis
        /// </summary>
        public RedisAdapter(IDatabase database, int expiryTimeSeconds, string sessionId = "", string cacheKeyPrefix = "")
        {
            SetDatabase(database);
            SetKeys(sessionId, cacheKeyPrefix);
            ExpiryTime = expiryTimeSeconds > 0 ? TimeSpan.FromSeconds(expiryTimeSeconds) : null;
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// Creates an connection with Redis database.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void SetDatabase(IDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("Database value can not be null.");
            }
            Database = database;
        }

        /// <summary>
        /// Creates Prefix for the key by appending sessionId and and a choosen prefix.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="cacheKeyPrefix"></param>
        private void SetKeys(string sessionId, string cacheKeyPrefix)
        {
            if(string.IsNullOrEmpty(sessionId))
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
        private string SerializeToJson<T>(T value)
        {
            return value == null ? string.Empty : JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Deserialize JSON string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T? DeserializeToObject<T>(RedisValue value)
        {
            var result = default(T);
            if(value.HasValue && value.Length() > 0)
            {
                result = JsonConvert.DeserializeObject<T>(value.ToString());
            }
            return result;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Sets value specific to the session to the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if successful</returns>
        public bool Set<T>(string key, T value)
        {
            var keyValue = ConstructKey(key);

            var serializedValue = SerializeToJson(value);

            var redisValue = new RedisValue(serializedValue);

            return Database.StringSet(keyValue, redisValue, ExpiryTime);
        }

        /// <summary>
        /// Gets value specific to the session from the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? Get<T>(string key)
        {
            var keyValue = ConstructKey(key);

            var redisValue = Database.StringGet(keyValue);

            return DeserializeToObject<T>(redisValue);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True if successful</returns>
        public bool Remove(string key)
        {
            var keyValue = ConstructKey(key);

            return Database.KeyDelete(keyValue);
        }

        /// <summary>
        /// Check if a key is present in the cache of current session
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>True if key found</returns>
        public bool IsSet(string key)
        {
            var keyValue = ConstructKey(key);

            return Database.KeyExists(keyValue);
        }

        #endregion Public Methods

    }
}
