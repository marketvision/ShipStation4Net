using Newtonsoft.Json;
using ShipStation4Net.Clients;
using System.IO;

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
