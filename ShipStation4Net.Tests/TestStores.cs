using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestStores : TestBase
    {
        [Fact]
        public async void TestGetStoresAsync()
        {
            var stores = await Client.Stores.GetItemsAsync() as List<Store>;

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetInactiveStoresAsync()
        {
            var stores = await Client.Stores.GetItemsAsync(true) as List<Store>;

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetStoreAsync()
        {
            var testStore = JsonConvert.DeserializeObject<Store>(File.ReadAllText("Results/store_test.json"));

            var stores = await Client.Stores.GetItemsAsync(false, testStore.StoreId) as List<Store>;

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetStoreRefreshStatusAsync()
        {
            var testStore = JsonConvert.DeserializeObject<Store>(File.ReadAllText("Results/store_test.json"));

            var refreshStatus = await Client.Stores.GetStoreRefreshStatusAsync(testStore.StoreId.Value);

            Assert.Equal(testStore.StoreId, refreshStatus.StoreId);
        }
    }
}
