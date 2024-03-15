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
    public class OrderItem : IEntity
    {
        /// <summary>
        /// The system generated identifier for the OrderItem. This is a read-only field.
        /// </summary>
        [JsonProperty("orderItemId")]
        public long? OrderItemId { get; set; }

        /// <summary>
        /// An identifier for the OrderItem in the originating system.
        /// </summary>
        [JsonProperty("lineItemKey")]
        public string LineItemKey { get; set; }

        /// <summary>
        /// The SKU (stock keeping unit) identifier for the product associated with this line item.
        /// </summary>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// The name of the product associated with this line item. Cannot be null
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The public URL to the product image.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The weight of a single item.
        /// </summary>
        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        /// <summary>
        /// The quantity of product ordered.
        /// </summary>
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// The sell price of a single item specified by the order source.
        /// </summary>
        [JsonProperty("unitPrice")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// The tax price of a single item specified by the order source.
        /// </summary>
        [JsonProperty("taxAmount")]
        public double? TaxAmount { get; set; }

        /// <summary>
        /// The shipping amount or price of a single item specified by the order source.
        /// </summary>
        [JsonProperty("shippingAmount")]
        public double? ShippingAmount { get; set; }

        /// <summary>
        /// The location of the product within the seller's warehouse (e.g. Aisle 3, Shelf A, Bin 5)
        /// </summary>
        [JsonProperty("warehouseLocation")]
        public string WarehouseLocation { get; set; }

        [JsonProperty("options")]
        public IList<ItemOption> Options { get; set; }

        /// <summary>
        /// The identifier for the Product Resource associated with this OrderItem.
        /// </summary>
        [JsonProperty("productId")]
        public int? ProductId { get; set; }

        /// <summary>
        /// The fulfillment SKU associated with this OrderItem if the fulfillment provider requires an identifier other then the SKU.
        /// </summary>
        [JsonProperty("fulfillmentSku")]
        public string FulfillmentSku { get; set; }

        /// <summary>
        /// Indicates that the OrderItem is a non-physical adjustment to the order (e.g. a discount or promotional code)
        /// </summary>
        [JsonProperty("adjustment")]
        public bool? IsAdjustment { get; set; }

        /// <summary>
        /// The Universal Product Code associated with this OrderItem.
        /// </summary>
        [JsonProperty("upc")]
        public string Upc { get; set; }
    }
}
