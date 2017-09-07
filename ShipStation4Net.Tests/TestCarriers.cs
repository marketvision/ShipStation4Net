using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestCarriers : TestBase
    {
        [Fact]
        public async void TestGetCarrier()
        {
            var carrier = new Carrier();
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            try
            {
                carrier = await Client.Carriers.GetAsync(testCarrier.Code);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                carrier = await Client.Carriers.GetAsync(testCarrier.Code);
            }

            Assert.Equal(carrier.AccountNumber, testCarrier.AccountNumber);
            Assert.Equal(carrier.Code, testCarrier.Code);
            Assert.Equal(carrier.Name, testCarrier.Name);
            Assert.Equal(carrier.RequiresFundedAccount, testCarrier.RequiresFundedAccount);
        }
        
        [Fact]
        public async void TestGetCarriers()
        {
            var carriers = new List<Carrier>();

            try
            {
                carriers = await Client.Carriers.GetItemsAsync() as List<Carrier>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                carriers = await Client.Carriers.GetItemsAsync() as List<Carrier>;
            }

            Assert.True(carriers.Count > 0);
        }
        
        [Fact]
        public async void TestGetCarrierPackages()
        {
            var packages = new List<Package>();
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            try
            {
                packages = await Client.Carriers.GetPackages(testCarrier.Code) as List<Package>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                packages = await Client.Carriers.GetPackages(testCarrier.Code) as List<Package>;
            }

            Assert.True(packages.Count > 0);
        }

        [Fact]
        public async void TestGetCarrierServices()
        {
            var packages = new List<Package>();
            var testCarrier = JsonConvert.DeserializeObject<Carrier>(File.ReadAllText("Results/carrier_test.json"));

            try
            {
                packages = await Client.Carriers.GetServices(testCarrier.Code) as List<Package>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                packages = await Client.Carriers.GetServices(testCarrier.Code) as List<Package>;
            }

            Assert.True(packages.Count > 0);
        }
    }
}
