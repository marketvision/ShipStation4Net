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

namespace ShipStation4Net.Domain.Entities
{
    public class ShipmentLabelRequest
    {
        /// <summary>
        /// Identifies the carrier to be used for this label.
        /// </summary>
        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        /// <summary>
        /// Identifies the shipping service to be used for this label.
        /// </summary>
        [JsonProperty("serviceCode")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Identifies the packing type that should be used for this label.
        /// </summary>
        [JsonProperty("packageCode")]
        public string PackageCode { get; set; }

        /// <summary>
        /// Identifies the delivery confirmation type to be used for this label.
        /// </summary>
        [JsonProperty("confirmation")]
        public string Confirmation { get; set; }

        /// <summary>
        /// The date the shipment will be shipped.
        /// </summary>
        [JsonProperty("shipDate")]
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Shipment's weight.
        /// </summary>
        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        /// <summary>
        /// Shipment's dimensions.
        /// </summary>
        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        /// <summary>
        /// Address indicating shipment's origin.
        /// </summary>
        [JsonProperty("shipFrom")]
        public Address ShipFrom { get; set; }

        /// <summary>
        /// Address indicating shipment's destination.
        /// </summary>
        [JsonProperty("shipTo")]
        public Address ShipTo { get; set; }

        /// <summary>
        /// The shipping insurance information associated with this order.
        /// </summary>
        [JsonProperty("insuranceOptions")]
        public object InsuranceOptions { get; set; }

        /// <summary>
        /// Customs information that can be used to generate customs documents for international orders. 
        /// </summary>
        [JsonProperty("internationalOptions")]
        public object InternationalOptions { get; set; }

        /// <summary>
        /// Various advanced options that may be available depending on the shipping carrier that is used to ship the order. 
        /// </summary>
        [JsonProperty("advancedOptions")]
        public object AdvancedOptions { get; set; }

        /// <summary>
        /// Specifies whether a test label should be created. Default value: false.
        /// </summary>
        [JsonProperty("testLabel")]
        public bool? TestLabel { get; set; }
    }
}
