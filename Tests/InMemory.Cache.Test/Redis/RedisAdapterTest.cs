using InMemory.Cache.Redis;
using Microsoft.Extensions.Configuration;

namespace InMemory.Cache.Test.Redis
{
    [TestClass]
    public class RedisAdapterTest
    {

        private CacheAdapterTest TestCacheAdapter { get; set; }

        public RedisAdapterTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var connectionString = configuration["redis:address"] + ":" + configuration["redis:port"] + ",allowAdmin=" + configuration["redis:allowAdmin"];
            var expiryTimeSeconds = Convert.ToInt32(configuration["redis:expiryTime"]);
            var keyPrefix = configuration["redis:CacheKeyPrefix"] ?? string.Empty;
            var sessionId = new Random().Next().ToString();

            var redisAdapterFactory = new RedisAdapterFactory(connectionString ?? string.Empty, expiryTimeSeconds, sessionId, keyPrefix ?? string.Empty);

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
