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
        int testFulfillmentId = 398825;

        [Fact]
        public async void TestGetPageOneFulfillments()
        {
            var fulfillments = await Client.Fulfillments.GetPageAsync(1) as List<Fulfillment>;

            Assert.True(fulfillments.Count > 0);
        }

        [Fact]
        public async void TestGetFulfillment()
        {
            var fulfillment = await Client.Fulfillments.GetAsync(testFulfillmentId);

            Assert.IsType<Fulfillment>(fulfillment);
            Assert.Equal(fulfillment.FulfillmentId, testFulfillmentId);
        }

        [Fact]
        public async void TestGetFulfillmentsWithCreateDateFilter()
        {
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
            var nameFilter = new FulfillmentsFilter
            {
                RecipientName = "John Hamilton"
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, nameFilter) as List<Fulfillment>;

            Assert.Equal("John Hamilton", fulfillments[0].ShipTo.Name);
        }

        [Fact]
        public async void TestGetFulfillmentsWithTrackingNumberFilter()
        {
            var trackingNoFilter = new FulfillmentsFilter
            {
                TrackingNumber = "1ZEF91370195769272"
            };

            var fulfillments = await Client.Fulfillments.GetPageAsync(1, 1, trackingNoFilter) as List<Fulfillment>;

            Assert.Equal("1ZEF91370195769272", fulfillments[0].TrackingNumber);
        }
    }
}
