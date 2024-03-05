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
