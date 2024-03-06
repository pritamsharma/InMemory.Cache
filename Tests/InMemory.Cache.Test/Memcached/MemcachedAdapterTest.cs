using InMemory.Cache.Memcached;

namespace InMemory.Cache.Test.Memcached
{

    [TestClass]
    public class MemcachedAdapterTest : CacheAdapterTestBase
    {

        public MemcachedAdapterTest()
        {
            var address = Configuration["memcached:address"] ?? string.Empty;
            var port = Convert.ToInt32(Configuration["memcached:port"] ?? string.Empty);
            var expiryTimeSeconds = Convert.ToInt32(Configuration["memcached:expiryTime"]);
            var keyPrefix = Configuration["memcached:CacheKeyPrefix"] ?? string.Empty;
            var sessionId = new Random().Next().ToString();

            var redisAdapterFactory = new MemcachedAdapterFactory(address, port, expiryTimeSeconds, sessionId, keyPrefix ?? string.Empty);

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
