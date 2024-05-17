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
    public class MarkOrderAsShippedRequest
    {
        /// <summary>
        /// Identifies the order that will be marked as shipped.
        /// </summary>
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// Code of the carrier that is marked as having shipped the order.
        /// </summary>
        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        /// <summary>
        /// Date order was shipped.
        /// </summary>
        [JsonProperty("shipDate")]
        public string ShipDate { get; set; }

        /// <summary>
        /// Tracking number of shipment.
        /// </summary>
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Specifies whether the customer should be notified of the shipment. Default value: false
        /// </summary>
        [JsonProperty("notifyCustomer")]
        public bool NotifyCustomer { get; set; }

        /// <summary>
        /// Specifies whether the sales channel should be notified of the shipment. Default value: false
        /// </summary>
        [JsonProperty("notifySalesChannel")]
        public bool NotifySalesChannel { get; set; }
    }

}
