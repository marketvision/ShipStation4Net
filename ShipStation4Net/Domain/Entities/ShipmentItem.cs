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
    public class ShipmentItem
    {
        [JsonProperty("orderItemId")]
        public long? OrderItemId { get; set; }

        [JsonProperty("lineItemKey")]
        public string LineItemKey { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public int? UnitPrice { get; set; }

        [JsonProperty("warehouseLocation")]
        public string WarehouseLocation { get; set; }

        //[JsonProperty("options")]
        //public string Options { get; set; }

        [JsonProperty("productId")]
        public int? ProductId { get; set; }

        [JsonProperty("fulfillmentSku")]
        public string FulfillmentSku { get; set; }
    }


}
