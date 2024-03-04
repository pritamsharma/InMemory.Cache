using InMemory.Cache.Interface;
using Newtonsoft.Json.Linq;

namespace InMemory.Cache.Test
{
    internal class CacheAdapterTest
    {

        ICacheAdapter CacheAdapter { get; set; }

        public CacheAdapterTest(ICacheAdapter adapter)
        {
            CacheAdapter = adapter;
        }

        private JObject GenerateTestData
        {
            get
            {
                var randomGenerater = new Random();

                dynamic testData = new JObject();
                testData.AgencyId = randomGenerater.Next();
                testData.EntityId = randomGenerater.Next();
                testData.EntityType = Guid.NewGuid().ToString();
                testData.ConsentValue = Guid.NewGuid().ToString();
                testData.TenantId = randomGenerater.Next();
                testData.SalesPersonId = randomGenerater.Next();
                testData.Success = randomGenerater.Next() % 2 == 0 ? "Yes" : "No";
                testData.CreatedBy = randomGenerater.Next();
                testData.CreatedDate = DateTime.Now.ToShortDateString();

                return testData;
            }
        }

        public void SetTestPositive()
        {
            var key = "SetTestPositive_Key";
            var isSuccess = CacheAdapter.Set(key, GenerateTestData);
            RemoveKey(key);
            Assert.IsTrue(isSuccess);
        }

        public void GetTestPositive()
        {
            var key = "GetTestPositive_Key";
            CacheAdapter.Set(key, GenerateTestData);
            var cacheValue = CacheAdapter.Get<JObject>(key);
            RemoveKey(key);
            Assert.IsTrue(cacheValue != null && cacheValue.HasValues);
        }

        public void GetTestNegative()
        {
            var key = "GetTestNegative_Key";
            var cacheValue = CacheAdapter.Get<JObject>(key);
            Assert.IsTrue(cacheValue == null || !cacheValue.HasValues);
        }

        public void RemoveTestPositive()
        {
            var key = "RemoveTestPositive_Key";
            CacheAdapter.Set(key, GenerateTestData);
            var isSuccess = RemoveKey(key);
            Assert.IsTrue(isSuccess);
        }

        public void RemoveTestNegative()
        {
            var key = "RemoveTestNegative_Key";
            var isSuccess = RemoveKey(key);
            Assert.IsFalse(isSuccess);
        }

        private bool RemoveKey(string key)
        {
            return CacheAdapter.Remove(key);
        }

        public void IsSetTestPositive()
        {
            var key = "IsSetTestPositive_Key";
            CacheAdapter.Set(key, GenerateTestData);
            var isSuccess = CacheAdapter.IsSet(key);
            CacheAdapter.Remove(key);
            Assert.IsTrue(isSuccess);
        }

        public void IsSetTestNegative()
        {
            var key = "IsSetTestNegative_Key";
            var isSuccess = CacheAdapter.IsSet(key);
            Assert.IsFalse(isSuccess);
        }

    }
}
