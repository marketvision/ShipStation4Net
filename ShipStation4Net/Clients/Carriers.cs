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

using Newtonsoft.Json.Linq;
using ShipStation4Net.Clients.Interfaces;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Carriers : ClientBase, IListsItems<Carrier>
    {
        public Carriers(Configuration configuration) : base(configuration)
        {
            BaseUri = "carriers";
        }

        /// <summary>
        /// Retrieves the shipping carrier account details for the specified carrierCode.Use this method to determine a carrier's account 
        /// balance.
        /// </summary>
        /// <param name="id">The requested carrier</param>
        /// <returns>The requested carrier</returns>
        public Task<Carrier> GetAsync(string carrierCode)
        {
            return GetDataAsync<Carrier>($"getcarrier?carrierCode={carrierCode}");
        }

        /// <summary>
        /// Lists all shipping providers connected to this account.
        /// </summary>
        /// <returns>A list of carriers</returns>
        public Task<IList<Carrier>> GetItemsAsync()
        {
            return GetDataAsync<IList<Carrier>>();
        }

        /// <summary>
        /// Adds funds to a carrier account using the payment information on file.
        /// </summary>
        /// <param name="carrierCode">The carrier to add funds to.</param>
        /// <param name="amount">The dollar amount to add to the account. The minimum value that can be added is $10.00. The maximum value
        /// is $10,000.00.</param>
        /// <returns>The changed carrier with the dollar amount added to it.</returns>
        public Task<Carrier> AddFundsAsync(string carrierCode, double amount)
        {
            var fundsRequest = new JObject();
            fundsRequest["carrierCode"] = carrierCode;
            fundsRequest["amount"] = amount;
            return PostDataAsync<JObject, Carrier>("addfunds", fundsRequest);
        }

        /// <summary>
        /// Retrieves a list of packages for the specified carrier
        /// </summary>
        /// <param name="carrierCode">The carrier's code</param>
        /// <returns>A list of packages for the specified carrier</returns>
        public Task<IList<Package>> GetPackages(string carrierCode)
        {
            return GetDataAsync<IList<Package>>($"listpackages?carrierCode={carrierCode}");
        }

        /// <summary>
        /// Retrieves the list of available shipping services provided by the specified carrier
        /// </summary>
        /// <param name="carrierCode">The carrier's code</param>
        /// <returns>The list of available shipping services provided by the specified carrier</returns>
        public Task<IList<Package>> GetServices(string carrierCode)
        {
            return GetDataAsync<IList<Package>>($"listservices?carrierCode={carrierCode}");
        }
    }
}
