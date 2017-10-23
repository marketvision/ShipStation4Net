using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestFulfillments : TestBase
    {
        [Fact]
        public async void TestGetPageOneFulfillments()
        {
            var fulfillments = await Client.Fulfillments.GetPageAsync(1) as List<Fulfillment>;

            Assert.True(fulfillments.Count > 0);
        }

        [Fact]
        public async void TestGetFulfillment()
        {
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var fulfillment = await Client.Fulfillments.GetAsync(testFulfillment.FulfillmentId.Value);

            Assert.Equal(JsonConvert.SerializeObject(testFulfillment), JsonConvert.SerializeObject(fulfillment));
        }

        [Fact]
        public async void TestGetFulfillmentsWithCreateDateFilter()
        {
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var createDateFilter = new FulfillmentsFilter
            {
                CreateDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy = FulfillmentsSortBy.CreateDate,
                SortDir = SortDir.Ascending
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, createDateFilter) as List<Fulfillment>;

            Assert.True(fulfillments.Count > 0);
        }

        [Fact]
        public async void TestGetFulfillmentsWithShipDateFilter()
        {
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var shipDateFilter = new FulfillmentsFilter
            {
                ShipDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy = FulfillmentsSortBy.ShipDate,
                SortDir = SortDir.Ascending
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 100, shipDateFilter) as List<Fulfillment>;

            Assert.True(fulfillments.Count > 0);
        }

        [Fact]
        public async void TestGetFulfillmentsWithNameFilter()
        {
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var nameFilter = new FulfillmentsFilter
            {
                RecipientName = testFulfillment.ShipTo.Name
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, nameFilter) as List<Fulfillment>;

            Assert.Equal(fulfillments[0].ShipTo.Name, testFulfillment.ShipTo.Name);
        }

        [Fact]
        public async void TestGetFulfillmentsWithTrackingNumberFilter()
        {
            var testFulfillment = JsonConvert.DeserializeObject<Fulfillment>(File.ReadAllText("Results/fulfillment_test.json"));

            var trackingNoFilter = new FulfillmentsFilter
            {
                TrackingNumber = testFulfillment.TrackingNumber
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, trackingNoFilter) as List<Fulfillment>;

            Assert.Equal(fulfillments[0].TrackingNumber, testFulfillment.TrackingNumber);
        }
    }
}
