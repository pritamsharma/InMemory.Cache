using InMemory.Cache.Redis;

namespace InMemory.Cache.Test.Redis
{
    [TestClass]
    public class RedisAdapterTest : CacheAdapterTestBase
    {

        public RedisAdapterTest() : base()
        {
            var redisAdapterFactory = new RedisAdapterFactory(
                configuration: Configuration["Redis:Connection"] ?? string.Empty,
                expiryTimeSeconds: Convert.ToInt32(Configuration["Redis:ExpiryTime"]),
                sessionId: new Random().Next().ToString(),
                cacheKeyPrefix: Configuration["Redis:CacheKeyPrefix"] ?? string.Empty);

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
