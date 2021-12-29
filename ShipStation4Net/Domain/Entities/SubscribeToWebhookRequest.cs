#region License
/*
 * Copyright 2021 MarketVision Consulting Group
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
using ShipStation4Net.Domain.Enumerations;
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class SubscribeToWebhookRequest
    {
        /// <summary>
        /// The URL to send the webhooks to
        /// </summary>
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        /// <summary>
        /// The type of webhook to subscribe to.
        /// </summary>
        [JsonProperty("event")]
        public WebhookEvents Event { get; set; }

        /// <summary>
        /// If passed in, the webhooks will only be triggered for this store_id
        /// </summary>
        [JsonProperty("store_id")]
        public int? StoreId { get; set; }

        /// <summary>
        /// Display name for the webhook
        /// </summary>
        [JsonProperty("friendly_name")]
        public string FriendlyName { get; set; }
    }
}
