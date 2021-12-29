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
using System;
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class Store : IEntity
    {
        [JsonProperty("storeId")]
        public int? StoreId { get; set; }

        [JsonProperty("storeName")]
        public string StoreName { get; set; }

        [JsonProperty("marketplaceId")]
        public int? MarketplaceId { get; set; }

        [JsonProperty("marketplaceName")]
        public string MarketplaceName { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("integrationUrl")]
        public string IntegrationUrl { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("publicEmail")]
        public string PublicEmail { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("refreshDate")]
        public DateTime? RefreshDate { get; set; }

        [JsonProperty("lastRefreshAttempt")]
        public DateTime? LastRefreshAttempt { get; set; }

        [JsonProperty("autoRefresh")]
        public bool? AutoRefresh { get; set; }

        [JsonProperty("statusMappings")]
        public IList<StatusMapping> StatusMappings { get; set; }
    }
    
}
