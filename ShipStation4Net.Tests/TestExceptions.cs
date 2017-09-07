using ShipStation4Net.Clients;
using ShipStation4Net.Exceptions;
using System;
using System.Threading.Tasks;
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
                try
                {
                    await client.Orders.GetAsync(1);
                }
                catch (ApiLimitReachedException ex)
                {
                    await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
                }
                finally
                {
                    await client.Orders.GetAsync(1);
                }
            });
        }

        [Fact]
        public async void TestThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                try
                {
                    await Client.Orders.GetAsync(1);
                }
                catch (ApiLimitReachedException ex)
                {
                    await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
                }
                finally
                {
                    await Client.Orders.GetAsync(1);
                }
            });
        }
    }
}
