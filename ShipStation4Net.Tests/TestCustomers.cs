using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Filters;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestCustomers : TestBase
    {
        int testCustomerId = 123745004;

        [Fact]
        public async void TestGetPageOneCustomers()
        {
            var customers = await Client.Customers.GetPageAsync(1) as List<Customer>;

            Assert.True(customers.Count > 0);
        }

        [Fact]
        public async void TestGetCustomersWithCustomersFilters()
        {
            var marketplaceFilter = new CustomersFilter { MarketplaceId = 13 };
            var stateFilter = new CustomersFilter { StateCode = "GA" };

            var customers = await Client.Customers.GetPageAsync(1, 100, marketplaceFilter) as List<Customer>;

            Assert.True(customers.Count > 0);

            customers = await Client.Customers.GetPageAsync(1, 100, stateFilter) as List<Customer>;

            Assert.True(customers.Count > 0);
        }

        [Fact]
        public async void TestGetCustomerAsync()
        {
            var customer = await Client.Customers.GetAsync(testCustomerId);

            Assert.IsType<Customer>(customer);
            Assert.Equal(testCustomerId, customer.CustomerId);
        }
    }
}
