using InMemory.Cache.Memcached;

namespace InMemory.Cache.Test.Memcached
{

    [TestClass]
    public class MemcachedAdapterTest : CacheAdapterTestBase
    {

        public MemcachedAdapterTest():base()
        {
            var redisAdapterFactory = new MemcachedAdapterFactory(
                address: Configuration["Memcached:Address"] ?? string.Empty, 
                port: Convert.ToInt32(Configuration["Memcached:Port"] ?? string.Empty), 
                expiryTimeSeconds: Convert.ToInt32(Configuration["Memcached:ExpiryTime"]), 
                sessionId: new Random().Next().ToString(), 
                cacheKeyPrefix: Configuration["Memcached:CacheKeyPrefix"] ?? string.Empty);

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
