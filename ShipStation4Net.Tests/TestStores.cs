using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using ShipStation4Net.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestStores : TestBase
    {
        [Fact]
        public async void TestGetStoresAsync()
        {
            var stores = new List<Store>();

            try
            {
                stores = await Client.Stores.GetItemsAsync() as List<Store>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                stores = await Client.Stores.GetItemsAsync() as List<Store>;
            }

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetInactiveStoresAsync()
        {
            var stores = new List<Store>();

            try
            {
                stores = await Client.Stores.GetItemsAsync(true) as List<Store>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                stores = await Client.Stores.GetItemsAsync(true) as List<Store>;
            }

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetStoreAsync()
        {
            var stores = new List<Store>();
            var testStore = JsonConvert.DeserializeObject<Store>(File.ReadAllText("Results/store_test.json"));

            try
            {
                stores = await Client.Stores.GetItemsAsync(false, testStore.StoreId) as List<Store>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                stores = await Client.Stores.GetItemsAsync(false, testStore.StoreId) as List<Store>;
            }

            Assert.True(stores.Count > 0);
        }

        [Fact]
        public async void TestGetStoreRefreshStatusAsync()
        {
            var refreshStatus = new StoreRefreshStatusResponse();
            var testStore = JsonConvert.DeserializeObject<Store>(File.ReadAllText("Results/store_test.json"));

            try
            {
                refreshStatus = await Client.Stores.GetStoreRefreshStatusAsync(testStore.StoreId.Value);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                refreshStatus = await Client.Stores.GetStoreRefreshStatusAsync(testStore.StoreId.Value);
            }

            Assert.Equal(testStore.StoreId, refreshStatus.StoreId);
        }
    }
}
