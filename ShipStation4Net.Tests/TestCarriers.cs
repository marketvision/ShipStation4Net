using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestCarriers : TestBase
    {
        string testCarrierCode = "stamps_com";

        [Fact]
        public async void TestGetCarrier()
        {
            var carrier = await Client.Carriers.GetAsync(testCarrierCode);

            Assert.IsType<Carrier>(carrier);
            Assert.Equal(testCarrierCode, carrier.Code);
        }

        [Fact]
        public async void TestGetCarriers()
        {
            var carriers = await Client.Carriers.GetItemsAsync() as List<Carrier>;

            Assert.True(carriers.Count > 0);
        }

        [Fact]
        public async void TestGetCarrierPackages()
        {
            var packages = await Client.Carriers.GetPackages(testCarrierCode) as List<Package>;

            Assert.True(packages.Count > 0);
        }

        [Fact]
        public async void TestGetCarrierServices()
        {
            var packages = await Client.Carriers.GetServices(testCarrierCode) as List<Package>;

            Assert.True(packages.Count > 0);
        }
    }
}
