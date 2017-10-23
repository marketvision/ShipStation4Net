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

namespace ShipStation4Net.Domain.Entities
{
    public class Warehouse : IEntity
    {
        [JsonProperty("warehouseId")]
        public int? WarehouseId { get; set; }

        /// <summary>
        /// Name of Ship From Location.
        /// </summary>
        [JsonProperty("warehouseName")]
        public string WarehouseName { get; set; }

        /// <summary>
        /// The origin address. Shipping rates will be calculated from this address. Use the Address model.
        /// </summary>
        [JsonProperty("originAddress")]
        public Address OriginAddress { get; set; }

        /// <summary>
        /// The return address. If a "returnAddress" is not specified, your "originAddress" will be used as your "returnAddress". Use the 
        /// Address model.
        /// </summary>
        [JsonProperty("returnAddress")]
        public Address ReturnAddress { get; set; }

        /// <summary>
        /// Specifies whether or not this will be your default Ship From Location.
        /// </summary>
        [JsonProperty("isDefault")]
        public bool? IsDefault { get; set; }

        [JsonProperty("sellerIntegrationId")]
        public string SellerIntegrationId { get; set; }

        [JsonProperty("extInventoryIdentity")]
        public string ExtInventoryIdentity { get; set; }
    }
    
}
