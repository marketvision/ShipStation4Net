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

using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class AdvancedOptions
    {
        /// <summary>
        /// Specifies the warehouse where to the order is to ship from. If the order was fulfilled using a fill provider, no warehouse is attached to these orders and will result in a null value being returned. *Please see note below
        /// </summary>
        [JsonProperty("warehouseId")]
        public int? WarehouseId { get; set; }

        /// <summary>
        /// Specifies whether the order is non-machinable.
        /// </summary>
        [JsonProperty("nonMachinable")]
        public bool? NonMachinable { get; set; }

        /// <summary>
        /// Specifies whether the order is to be delivered on a Saturday.
        /// </summary>
        [JsonProperty("saturdayDelivery")]
        public bool? SaturdayDelivery { get; set; }

        /// <summary>
        /// Specifies whether the order contains alcohol.
        /// </summary>
        [JsonProperty("containsAlcohol")]
        public bool? ContainsAlcohol { get; set; }

        /// <summary>
        /// ID of store that is associated with the order. If not specified in the CreateOrder call either to create or update an order, ShipStation will default to the first manual store on the account.
        /// </summary>
        [JsonProperty("storeId")]
        public int? StoreId { get; set; }

        /// <summary>
        /// Field that allows for custom data to be associated with an order. *Please see note below
        /// </summary>
        [JsonProperty("customField1")]
        public string CustomField1 { get; set; }

        /// <summary>
        /// Field that allows for custom data to be associated with an order. *Please see note below
        /// </summary>
        [JsonProperty("customField2")]
        public string CustomField2 { get; set; }

        /// <summary>
        /// Field that allows for custom data to be associated with an order. *Please see note below
        /// </summary>
        [JsonProperty("customField3")]
        public string CustomField3 { get; set; }

        /// <summary>
        /// Identifies the original source/marketplace of the order. *Please see note below
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Read-Only: Returns whether or not an order has been merged or split with another order. Read Only
        /// </summary>
        [JsonProperty("mergedOrSplit")]
        public bool? MergedOrSplit { get; set; }

        /// <summary>
        /// Read-Only: Array of orderIds. Each orderId identifies an order that was merged with the associated order. Read Only
        /// </summary>
        [JsonProperty("mergedIds")]
        public IList<object> MergedIds { get; set; }

        /// <summary>
        /// Read-Only: If an order has been split, it will return the Parent ID of the order with which it has been split. If the order has not been split, this field will return null. Read Only
        /// </summary>
        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        /// <summary>
        /// Identifies which party to bill. Possible values: "my_account", **"my_other_account", "recipient", "third_party".
        /// </summary>
        [JsonProperty("billToParty")]
        public string BillToParty { get; set; }

        /// <summary>
        /// Account number of billToParty.
        /// </summary>
        [JsonProperty("billToAccount")]
        public string BillToAccount { get; set; }

        /// <summary>
        /// Postal Code of billToParty.
        /// </summary>
        [JsonProperty("billToPostalCode")]
        public string BillToPostalCode { get; set; }

        /// <summary>
        /// Country Code of billToParty.
        /// </summary>
        [JsonProperty("billToCountryCode")]
        public string BillToCountryCode { get; set; }
        
        /// <summary>
        /// Country Code of billToMyOtherAccount.
        /// </summary>
        [JsonProperty("billToMyOtherAccount")]
        public string BillToMyOtherAccount { get; set; }
    }
}
