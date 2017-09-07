using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestCustomers : TestBase
    {
        [Fact]
        public async void TestGetPageOneCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                customers = await Client.Customers.GetPageAsync(1) as List<Customer>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                customers = await Client.Customers.GetPageAsync(1) as List<Customer>;
            }

            Assert.True(customers.Count > 0);
        }

        [Fact]
        public async void TestGetCustomersWithCustomersFilters()
        {
            var customers = new List<Customer>();
            var marketplaceFilter = new CustomersFilter { MarketplaceId = 13 };
            var stateFilter = new CustomersFilter { StateCode = "GA" };

            try
            {
                customers = await Client.Customers.GetPageAsync(1, 100, marketplaceFilter) as List<Customer>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                customers = await Client.Customers.GetPageAsync(1, 100, marketplaceFilter) as List<Customer>;
            }

            Assert.True(customers.Count > 0);

            try
            {
                customers = await Client.Customers.GetPageAsync(1, 100, stateFilter) as List<Customer>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                customers = await Client.Customers.GetPageAsync(1, 100, stateFilter) as List<Customer>;
            }

            Assert.True(customers.Count > 0);
        }

        [Fact]
        public async void TestGetCustomerAsync()
        {
            var carrier = new Customer();
            var testCustomer = JsonConvert.DeserializeObject<Customer>(File.ReadAllText("Results/customer_test.json"));

            try
            {
                carrier = await Client.Customers.GetAsync(testCustomer.CustomerId);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                carrier = await Client.Customers.GetAsync(testCustomer.CustomerId);
            }

            Assert.Equal(JsonConvert.SerializeObject(carrier), JsonConvert.SerializeObject(testCustomer));
        }
    }
}
