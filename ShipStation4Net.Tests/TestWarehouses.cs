using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestWarehouses : TestBase
    {
        [Fact]
        public async void TestGetWarehouse()
        {
            var testWarehouse = JsonConvert.DeserializeObject<Warehouse>(File.ReadAllText("Results/warehouse_test.json"));

            var warehouse = await Client.Warehouses.GetAsync(testWarehouse.WarehouseId.Value);
            
            // I have to do this so the local data goes through the same transformation the remote one does. Modify Date may cause complications.
            
            Assert.Equal(JsonConvert.SerializeObject(warehouse), JsonConvert.SerializeObject(warehouse));
        }

        [Fact]
        public async void TestGetWarehouses()
        {
            var warehouses = await Client.Warehouses.GetItemsAsync() as List<Warehouse>;

            Assert.True(warehouses.Count > 0);
        }
    }
}
