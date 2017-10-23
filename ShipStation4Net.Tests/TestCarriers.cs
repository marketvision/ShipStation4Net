using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestCarriers : TestBase
    {
        [Fact]
        public async void TestGetCarrier()
        {
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            var carrier = await Client.Carriers.GetAsync(testCarrier.Code);

            Assert.Equal(carrier.AccountNumber, testCarrier.AccountNumber);
            Assert.Equal(carrier.Code, testCarrier.Code);
            Assert.Equal(carrier.Name, testCarrier.Name);
            Assert.Equal(carrier.RequiresFundedAccount, testCarrier.RequiresFundedAccount);
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
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            var packages = await Client.Carriers.GetPackages(testCarrier.Code) as List<Package>;

            Assert.True(packages.Count > 0);
        }

        [Fact]
        public async void TestGetCarrierServices()
        {
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            var packages = await Client.Carriers.GetServices(testCarrier.Code) as List<Package>;

            Assert.True(packages.Count > 0);
        }
    }
}
