using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestUsers : TestBase
    {
        [Fact]
        public async void TestGetUsersAsync()
        {
            var users = new List<User>();

            try
            {
                users = await Client.Users.GetItemsAsync() as List<User>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                users = await Client.Users.GetItemsAsync() as List<User>;
            }

            Assert.True(users.Count > 0);
        }
    }
}
