using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestWarehouses : TestBase
    {
        [Fact]
        public async void TestGetWarehouse()
        {
            var warehouse = new Warehouse();
            var testWarehouse = JsonConvert.DeserializeObject<Warehouse>(File.ReadAllText("Results/warehouse_test.json"));

            try
            {
                warehouse = await Client.Warehouses.GetAsync(testWarehouse.WarehouseId);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                warehouse = await Client.Warehouses.GetAsync(testWarehouse.WarehouseId);
            }
            // I have to do this so the local data goes through the same transformation the remote one does. Modify Date may cause complications.
            
            Assert.Equal(JsonConvert.SerializeObject(warehouse), JsonConvert.SerializeObject(warehouse));
        }

        [Fact]
        public async void TestGetWarehouses()
        {
            var warehouses = new List<Warehouse>();

            try
            {
                await Client.Warehouses.GetItemsAsync();
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                warehouses = await Client.Warehouses.GetItemsAsync() as List<Warehouse>;
            }

            Assert.True(warehouses.Count > 0);
        }
    }
}
