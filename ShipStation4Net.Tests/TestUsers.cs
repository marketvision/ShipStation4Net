using ShipStation4Net.Domain.Entities;
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
            var users = await Client.Users.GetItemsAsync() as List<User>;

            Assert.True(users.Count > 0);
        }
    }
}
