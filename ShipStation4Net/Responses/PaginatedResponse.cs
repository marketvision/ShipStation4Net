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
using ShipStation4Net.Converters;
using ShipStation4Net.Domain.Entities;
using System.Collections.Generic;

namespace ShipStation4Net.Responses
{
    [JsonConverter(typeof(DynamicPropertyNameConverter))]
    public class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        public PaginatedResponse()
        {
            Items = new List<T>();
        }

        [JsonProperty("total")]
        public int TotalPages { get; set; }

        [JsonProperty("page")]
        public int CurrentPage { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonPropertyNameByType("customers", typeof(IList<Customer>))]
        [JsonPropertyNameByType("fulfillments", typeof(IList<Fulfillment>))]
        [JsonPropertyNameByType("orders", typeof(IList<Order>))]
        [JsonPropertyNameByType("products", typeof(IList<Product>))]
        [JsonPropertyNameByType("shipments", typeof(IList<Shipment>))]
        public virtual IList<T> Items { get; set; }
    }
}
