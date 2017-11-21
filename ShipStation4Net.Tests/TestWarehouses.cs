using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestWarehouses : TestBase
    {
        int testWarehouseId = 8893;

        [Fact]
        public async void TestGetWarehouse()
        {
            var warehouse = await Client.Warehouses.GetAsync(testWarehouseId);
            
            Assert.IsType<Warehouse>(warehouse);
            Assert.Equal(testWarehouseId, warehouse.WarehouseId);
        }

        [Fact]
        public async void TestGetWarehouses()
        {
            var warehouses = await Client.Warehouses.GetItemsAsync() as List<Warehouse>;

            Assert.True(warehouses.Count > 0);
        }
    }
}
