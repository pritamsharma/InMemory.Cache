using InMemory.Cache.Redis;

namespace InMemory.Cache.Test.Redis
{
    [TestClass]
    public class RedisAdapterTest : CacheAdapterTestBase
    {

        public RedisAdapterTest()
        {
            var connectionString = Configuration["redis:address"] + ":" + Configuration["redis:port"] + ",allowAdmin=" + Configuration["redis:allowAdmin"];
            var expiryTimeSeconds = Convert.ToInt32(Configuration["redis:expiryTime"]);
            var keyPrefix = Configuration["redis:CacheKeyPrefix"] ?? string.Empty;
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
