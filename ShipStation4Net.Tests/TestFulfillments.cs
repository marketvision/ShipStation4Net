using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Exceptions;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestFulfillments : TestBase
    {
        [Fact]
        public async void TestGetPageOneFulfillments()
        {
            var fulfillments = new List<Fulfillment>();

            try
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1) as List<Fulfillment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1) as List<Fulfillment>;
            }

            Assert.True(fulfillments.Count > 0);
        }

        [Fact]
        public async void TestGetFulfillment()
        {
            var fulfillment = new Fulfillment();
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            try
            {
                fulfillment = await Client.Fulfillments.GetAsync(testFulfillment.FulfillmentId);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillment = await Client.Fulfillments.GetAsync(testFulfillment.FulfillmentId);
            }

            Assert.Equal(JsonConvert.SerializeObject(testFulfillment), JsonConvert.SerializeObject(fulfillment));
        }

        [Fact]
        public async void TestGetFulfillmentsWithCreateDateFilter()
        {
            var fulfillments = new List<Fulfillment>();
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var createDateFilter = new FulfillmentsFilter
            {
                CreateDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy = FulfillmentsSortBy.CreateDate,
                SortDir = SortDir.Ascending
            };

            try
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, createDateFilter) as List<Fulfillment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, createDateFilter) as List<Fulfillment>;
            }

            Assert.True(fulfillments.Count > 0);
        }
        
        [Fact]
        public async void TestGetFulfillmentsWithShipDateFilter()
        {
            var fulfillments = new List<Fulfillment>();
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var shipDateFilter = new FulfillmentsFilter
            {
                ShipDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy = FulfillmentsSortBy.ShipDate,
                SortDir = SortDir.Ascending
            };

            try
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, shipDateFilter) as List<Fulfillment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, shipDateFilter) as List<Fulfillment>;
            }

            Assert.True(fulfillments.Count > 0);
        }
        
        [Fact]
        public async void TestGetFulfillmentsWithNameFilter()
        {
            var fulfillments = new List<Fulfillment>();
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var nameFilter = new FulfillmentsFilter
            {
                RecipientName = testFulfillment.ShipTo.Name
            };

            try
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, nameFilter) as List<Fulfillment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, nameFilter) as List<Fulfillment>;
            }

            Assert.Equal(fulfillments[0].ShipTo.Name, testFulfillment.ShipTo.Name);
        }

        [Fact]
        public async void TestGetFulfillmentsWithTrackingNumberFilter()
        {
            var fulfillments = new List<Fulfillment>();
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var trackingNoFilter = new FulfillmentsFilter
            {
                TrackingNumber = testFulfillment.TrackingNumber
            };

            try
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, trackingNoFilter) as List<Fulfillment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, trackingNoFilter) as List<Fulfillment>;
            }

            Assert.Equal(fulfillments[0].TrackingNumber, testFulfillment.TrackingNumber);
        }
    }
}
