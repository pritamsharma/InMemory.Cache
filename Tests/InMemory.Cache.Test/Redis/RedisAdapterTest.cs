using InMemory.Cache.Redis;
using Microsoft.Extensions.Configuration;

namespace InMemory.Cache.Test.Redis
{
    [TestClass]
    public class RedisAdapterTest : CacheAdapterTestBase
    {

        public RedisAdapterTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var connectionString = configuration["redis:address"] + ":" + configuration["redis:port"] + ",allowAdmin=" + configuration["redis:allowAdmin"];
            var expiryTimeSeconds = Convert.ToInt32(configuration["redis:expiryTime"]);
            var keyPrefix = configuration["redis:CacheKeyPrefix"] ?? string.Empty;
            var sessionId = new Random().Next().ToString();

            var redisAdapterFactory = new RedisAdapterFactory(connectionString ?? string.Empty, expiryTimeSeconds, sessionId, keyPrefix ?? string.Empty);

            CacheAdapter = redisAdapterFactory.CreateCacheAdapter();
        }

        [TestMethod]
        public override void SetTestPositive()
        {
            base.SetTestPositive();
        }

        [TestMethod]
        public override void GetTestPositive()
        {
            base.GetTestPositive();
        }

        [TestMethod]
        public override void GetTestNegative()
        {
            base.GetTestNegative();
        }

        [TestMethod]
        public override void RemoveTestPositive()
        {
            base.RemoveTestPositive();
        }

        [TestMethod]
        public override void RemoveTestNegative()
        {
            base.RemoveTestNegative();
        }

        [TestMethod]
        public override void IsSetTestPositive()
        {
            base.IsSetTestPositive();
        }

        [TestMethod]
        public override void IsSetTestNegative()
        {
            base.IsSetTestNegative();
        }

    }
}
