using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestAccounts : TestBase
    {
        [Fact]
        public async void TestListAccountTags()
        {
            var accountTags = await Client.Accounts.GetTagsAsync() as List<Tag>;

            Assert.True(accountTags.Count > 0);
        }
    }
}
