using InMemory.Cache.Memcached;
using Microsoft.Extensions.Configuration;

namespace InMemory.Cache.Test.Memcached
{
    [TestClass]
    public class MemcachedAdapterTest
    {
        private CacheAdapterTest TestCacheAdapter { get; set; }

        public MemcachedAdapterTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var address = configuration["memcached:address"] ?? string.Empty;
            var port = Convert.ToInt32(configuration["memcached:port"] ?? string.Empty);
            var expiryTimeSeconds = Convert.ToInt32(configuration["memcached:expiryTime"]);
            var keyPrefix = configuration["memcached:CacheKeyPrefix"] ?? string.Empty;
            var sessionId = new Random().Next().ToString();

            var redisAdapterFactory = new MemcachedAdapterFactory(address, port, expiryTimeSeconds, sessionId, keyPrefix ?? string.Empty);

            var redisAdapter = redisAdapterFactory.CreateCacheAdapter();

            TestCacheAdapter = new CacheAdapterTest(redisAdapter);
        }

        [TestMethod]
        public void SetTestPositive()
        {
            TestCacheAdapter.SetTestPositive();
        }

        [TestMethod]
        public void GetTestPositive()
        {
            TestCacheAdapter.GetTestPositive();
        }

        [TestMethod]
        public void GetTestNegative()
        {
            TestCacheAdapter.GetTestNegative();
        }

        [TestMethod]
        public void RemoveTestPositive()
        {
            TestCacheAdapter.RemoveTestPositive();
        }

        [TestMethod]
        public void RemoveTestNegative()
        {
            TestCacheAdapter.RemoveTestNegative();
        }

        [TestMethod]
        public void IsSetTestPositive()
        {
            TestCacheAdapter.IsSetTestPositive();
        }

        [TestMethod]
        public void IsSetTestNegative()
        {
            TestCacheAdapter.IsSetTestNegative();
        }

    }
}
