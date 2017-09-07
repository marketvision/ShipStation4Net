using Newtonsoft.Json;
using ShipStation4Net.Clients;
using ShipStation4Net.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestBase
    {
        protected readonly Configuration Configuration;
        protected ShipStationClient Client;

        public TestBase()
        {
            Client = new ShipStationClient(GetConfigurationFromFile());
        }

        protected Configuration GetConfigurationFromFile()
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("configuration.json"));
        }
    }
}
