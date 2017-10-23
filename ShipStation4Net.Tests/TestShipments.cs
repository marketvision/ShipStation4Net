using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestShipments : TestBase
    {
        [Fact]
        public async void TestGetPageOneShipmentsAsync()
        {
            var shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestShipmentFilterCreateDate()
        {
            var shipmentFilter = new ShipmentsFilter
            {
                CreateDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy = ShipmentsSortBy.CreateDate,
                SortDir = SortDir.Ascending
            };

            var shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestShipmentFilterCarrierCode()
        {
            var shipmentFilter = new ShipmentsFilter
            {
                CarrierCode = "ups",
                SortBy = ShipmentsSortBy.ShipDate,
                SortDir = SortDir.Descending
            };

            var shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestGetRatesAsync()
        {
            // This is the example from the API docs
            var ratesRequest = new RatesRequest
            {
                CarrierCode = "fedex",
                FromPostalCode = "78703",
                ToState = "DC",
                ToCountry = "US",
                ToPostalCode = "20500",
                ToCity = "Washington",
                Weight = new Weight
                {
                    Value = 3,
                    Units = WeightUnits.Ounces
                },
                Dimensions = new Dimensions
                {
                    Units = DimensionsUnits.Inches,
                    Length = 7,
                    Width = 5,
                    Height = 6
                },
                Confirmation = "delivery",
                IsResidential = false
            };

            var rates = await Client.Shipments.GetRatesAsync(ratesRequest) as List<Rate>;

            Assert.True(rates.Count > 0);
        }
    }
}
