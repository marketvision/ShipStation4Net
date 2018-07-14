using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestStores : TestBase
    {
        int testStoreId = 10162;
        int testMarketplaceId = 13;

        [Fact]
        public async void TestGetStoresAsync()
        {
            var stores = await Client.Stores.GetItemsAsync() as List<Store>;

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetStoreAsync()
        {
            var store = await Client.Stores.GetAsync(testStoreId);

            Assert.IsType<Store>(store);
            Assert.Equal(10162, store.StoreId);
        }

        [Fact]
        public async void TestGetInactiveStoresAsync()
        {
            var stores = await Client.Stores.GetItemsAsync(true) as List<Store>;

            Assert.True(stores.Count > 0);
        }
		[Fact]
        public async void TestGetStoreByMarketplaceAsync()
        {
            var stores = await Client.Stores.GetItemsAsync(false, testMarketplaceId) as List<Store>;

            Assert.True(stores.Count > 0);
        }


		[Fact]
		public async void TestGetRefreshAllStoresAsync()
		{
			var stores = await Client.Stores.RefreshAllStoresAsync();

			Assert.True(stores);
		}

		[Fact]
        public async void TestGetStoreRefreshStatusAsync()
        {
            var refreshStatus = await Client.Stores.GetStoreRefreshStatusAsync(testStoreId);

            Assert.Equal(testStoreId, refreshStatus.StoreId);
        }
    }
}
