using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestOrders : TestBase
    {
        [Fact]
        public async void TestGetOrder()
        {
            Order order = new Order();
            var testOrder = JsonConvert.DeserializeObject<Order>(File.ReadAllText("Results/order_test.json"));

            try
            {
                order = await Client.Orders.GetAsync(testOrder.OrderId.Value);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                order = await Client.Orders.GetAsync(testOrder.OrderId.Value);
            }

            Assert.Equal(JsonConvert.SerializeObject(testOrder), JsonConvert.SerializeObject(order));
        }

        [Fact]
        public async void TestGetPageOneOrders()
        {
            var orders = new List<Order>();

            try
            {
                orders = await Client.Orders.GetPageAsync(1) as List<Order>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                orders = await Client.Orders.GetPageAsync(1) as List<Order>;
            }

            Assert.True(orders.Count > 0);
        }

        [Fact]
        public async void TestGetOrdersByTag()
        {
            var orders = new List<Order>();
            var tags = await Client.Accounts.GetTagsAsync();

            try
            {
                orders = await Client.Orders.ListOrdersByTagAsync(tags[0].TagId.Value, OrderStatus.Shipped) as List<Order>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                orders = await Client.Orders.ListOrdersByTagAsync(tags[0].TagId.Value, OrderStatus.Shipped) as List<Order>;
            }

            Assert.True(orders.Count > 0);
        }
    }

}