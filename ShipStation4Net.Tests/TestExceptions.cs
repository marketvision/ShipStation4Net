using ShipStation4Net.Clients;
using ShipStation4Net.Exceptions;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestExceptions : TestBase
    {
        [Fact]
        public async void TestThrowsUnauthorizedException()
        {
            await Assert.ThrowsAsync<NotAuthorizedException>(async () =>
            {
                var client = new ShipStationClient(new Configuration
                {
                    UserName = "incorrect",
                    UserApiKey = "incorrect"
                });

                await client.Orders.GetAsync(1);
            });
        }

        [Fact]
        public async void TestThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await Client.Orders.GetAsync(1);
            });
        }
    }
}
