using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestAccounts : TestBase
    {
        [Fact]
        public async void TestListAccountTags()
        {
            var accountTags = new List<Tag>();

            try
            {
                accountTags = await Client.Accounts.GetTagsAsync() as List<Tag>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                accountTags = await Client.Accounts.GetTagsAsync() as List<Tag>;
            }

            Assert.True(accountTags.Count > 0);
        }
    }
}
