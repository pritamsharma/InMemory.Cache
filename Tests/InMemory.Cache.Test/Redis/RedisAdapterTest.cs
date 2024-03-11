using InMemory.Cache.Redis;

namespace InMemory.Cache.Test.Redis
{
    [TestClass]
    public class RedisAdapterTest : CacheAdapterTestBase
    {

        public RedisAdapterTest() : base()
        {
            var connectionString = Configuration["redis:address"] + ":" + Configuration["redis:port"] + ",allowAdmin=" + Configuration["redis:allowAdmin"];
            var expiryTimeSeconds = Convert.ToInt32(Configuration["redis:expiryTime"]);
            var keyPrefix = Configuration["redis:CacheKeyPrefix"] ?? string.Empty;
            var sessionId = new Random().Next().ToString();

            var redisAdapterFactory = new RedisAdapterFactory(connectionString ?? string.Empty, expiryTimeSeconds, sessionId, keyPrefix ?? string.Empty);

            CacheAdapter = redisAdapterFactory.CreateCacheAdapter();
        }

        [TestMethod]
        public override async Task SetTestPositive()
        {
            await base.SetTestPositive();
        }

        [TestMethod]
        public override async Task GetTestPositive()
        {
            await base.GetTestPositive();
        }

        [TestMethod]
        public override async Task GetTestNegative()
        {
            await base.GetTestNegative();
        }

        [TestMethod]
        public override async Task RemoveTestPositive()
        {
            await base.RemoveTestPositive();
        }

        [TestMethod]
        public override async Task RemoveTestNegative()
        {
            await base.RemoveTestNegative();
        }

        [TestMethod]
        public override async Task IsSetTestPositive()
        {
            await base.IsSetTestPositive();
        }

        [TestMethod]
        public override async Task IsSetTestNegative()
        {
            await base.IsSetTestNegative();
        }

    }
}
