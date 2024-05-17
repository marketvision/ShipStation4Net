using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestOrders : TestBase
    {
        long testOrderId = 14920239;

        [Fact]
        public async void TestGetOrder()
        {
            var order = await Client.Orders.GetAsync(testOrderId);

            Assert.IsType<Order>(order);
            Assert.Equal(testOrderId, order.OrderId);
        }

        [Fact]
        public async void TestGetPageOneOrders()
        {
            var orders = await Client.Orders.GetPageAsync(1) as List<Order>;

            Assert.True(orders.Count > 0);
        }

        [Fact]
        public async void TestGetOrdersByTag()
        {
            var tags = await Client.Accounts.GetTagsAsync();

            var orders = await Client.Orders.ListOrdersByTagAsync(tags[0].TagId.Value, OrderStatus.Shipped) as List<Order>;

            Assert.True(orders.Count > 0);
        }
    }

}
