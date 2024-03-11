using InMemory.Cache.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InMemory.Cache.Test
{
    public abstract class CacheAdapterTestBase
    {

        internal ICacheAdapter CacheAdapter { get; set; }

        internal IConfigurationRoot Configuration { get; set; }

        public CacheAdapterTestBase()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        internal JObject GenerateTestData
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

        public virtual async Task SetTestPositive()
        {
            var key = "SetTestPositive_Key";

            var isSuccess = await SetValue(key);

            _ = await RemoveKey(key);

            Assert.IsTrue(isSuccess);
        }

        internal async Task<bool> SetValue(string key)
        {
            return await CacheAdapter.SetAsync(key, GenerateTestData);
        }

        public virtual async Task GetTestPositive()
        {
            var key = "GetTestPositive_Key";

            _ = await SetValue(key);

            var cacheValue = await CacheAdapter.GetAsync<JObject>(key);

            _ = await RemoveKey(key);

            Assert.IsTrue(cacheValue != null && cacheValue.HasValues);
        }

        public virtual async Task GetTestNegative()
        {
            var key = "GetTestNegative_Key";

            var cacheValue = await CacheAdapter.GetAsync<JObject>(key);

            Assert.IsTrue(cacheValue == null || !cacheValue.HasValues);
        }

        public virtual async Task RemoveTestPositive()
        {
            var key = "RemoveTestPositive_Key";

            _ = await SetValue(key);

            var isSuccess = await RemoveKey(key);

            Assert.IsTrue(isSuccess);
        }

        public virtual async Task RemoveTestNegative()
        {
            var key = "RemoveTestNegative_Key";

            var isSuccess = await RemoveKey(key);

            Assert.IsFalse(isSuccess);
        }

        internal async Task<bool> RemoveKey(string key)
        {
            return await CacheAdapter.RemoveAsync(key);
        }

        public virtual async Task IsSetTestPositive()
        {
            var key = "IsSetTestPositive_Key";

            _ = await SetValue(key);

            var isSuccess = await CacheAdapter.IsSetAsync(key);

            _ = await RemoveKey(key);

            Assert.IsTrue(isSuccess);
        }

        public virtual async Task IsSetTestNegative()
        {
            var key = "IsSetTestNegative_Key";

            var isSuccess = await CacheAdapter.IsSetAsync(key);

            Assert.IsFalse(isSuccess);
        }

    }
}
