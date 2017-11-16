#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using ShipStation4Net.Clients.Interfaces;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Accounts : ClientBase, ICreates<User>
    {
        public Accounts(Configuration configuration) : base(configuration)
        {
            BaseUri = "accounts";
        }

        /// <summary>
        /// Creates a new ShipStation account and generates an apiKey and apiSecret to be used by the newly created account. PLEASE NOTE: 
        /// This endpoint does not require API key and API Secret credentials. The Authorization header can be left off. Use of this 
        /// specific endpoint requires approval, and is meant only for direct partners of ShipStation. This is the only endpoint to 
        /// require approval. All other endpoints listed in this document can be accessed by submitting proper authorization 
        /// credentials in the header of the request. To become a direct partner of ShipStation, or to request more information on 
        /// becoming a direct partner, we recommend reaching out to our Partners and Integrations team here: 
        /// https://info.shipstation.com/become-a-partner-api-and-custom-store-integrations
        /// </summary>
        /// <param name="newItem">The data for a new account.</param>
        /// <returns>The new account with fields updated by ShipStation.</returns>
        public async Task<User> CreateAsync(User newItem)
        {
            var accountCreatedResponse = await PostDataAsync<User, AccountCreatedResponse>("registeraccount", newItem).ConfigureAwait(false);
            if (!accountCreatedResponse.Success)
            {
                return null;
            }
            return newItem;
        }

        /// <summary>
        /// Lists all tags defined for this account.
        /// </summary>
        /// <returns>All tags defined for this account.</returns>
        public Task<IList<Tag>> GetTagsAsync()
        {
            return GetDataAsync<IList<Tag>>("listtags");
        }
    }
}
