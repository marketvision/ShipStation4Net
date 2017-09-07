using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Exceptions;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestShipments : TestBase
    {
        [Fact]
        public async void TestGetPageOneShipmentsAsync()
        {
            var shipments = new List<Shipment>();

            try
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestShipmentFilterCreateDate()
        {
            var shipments = new List<Shipment>();
            var shipmentFilter = new ShipmentsFilter
            {
                CreateDateStart = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                SortBy =  ShipmentsSortBy.CreateDate,
                SortDir = SortDir.Ascending
            };

            try
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestShipmentFilterCarrierCode()
        {
            var shipments = new List<Shipment>();
            var shipmentFilter = new ShipmentsFilter
            {
                CarrierCode = "ups",
                SortBy = ShipmentsSortBy.ShipDate,
                SortDir = SortDir.Descending
            };

            try
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                shipments = await Client.Shipments.GetPageAsync(1) as List<Shipment>;
            }

            Assert.True(shipments.Count > 0);
        }

        [Fact]
        public async void TestGetRatesAsync()
        {
            var rates = new List<Rate>();

            // This is the example from the API docs
            var ratesRequest = new RatesRequest
            {
                CarrierCode = "fedex",
                FromPostalCode = "78703",
                ToState = "DC",
                ToCountry = "US",
                ToPostalCode = "20500",
                ToCity = "Washington",
                Weight = new Weight {
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

            try
            {
                rates = await Client.Shipments.GetRatesAsync(ratesRequest) as List<Rate>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                rates = await Client.Shipments.GetRatesAsync(ratesRequest) as List<Rate>;
            }

            Assert.True(rates.Count > 0);
        }
    }
}
