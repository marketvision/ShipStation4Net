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
    public class RatesRequest
    {
        /// <summary>
        /// Returns rates for the specified carrier.
        /// </summary>
        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        /// <summary>
        /// Returns rates for the specified shipping service.
        /// </summary>
        [JsonProperty("serviceCode")]
        public object ServiceCode { get; set; }

        /// <summary>
        /// Returns rates for the specified package type.
        /// </summary>
        [JsonProperty("packageCode")]
        public object PackageCode { get; set; }

        /// <summary>
        /// Originating postal code.
        /// </summary>
        [JsonProperty("fromPostalCode")]
        public string FromPostalCode { get; set; }

        /// <summary>
        /// Destination State/Province. Please use two-character state/province abbreviation. Note this field is required for the 
        /// following carriers: UPS
        /// </summary>
        [JsonProperty("toState")]
        public string ToState { get; set; }

        /// <summary>
        /// Destination Country. Please use the two-character ISO country code.
        /// </summary>
        [JsonProperty("toCountry")]
        public string ToCountry { get; set; }

        /// <summary>
        /// Destination Postal Code.
        /// </summary>
        [JsonProperty("toPostalCode")]
        public string ToPostalCode { get; set; }

        /// <summary>
        /// Destination City.
        /// </summary>
        [JsonProperty("toCity")]
        public string ToCity { get; set; }

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
        /// Returns rates that account for the specified delivery confirmation type.
        /// </summary>
        [JsonProperty("confirmation")]
        public string Confirmation { get; set; }

        /// <summary>
        /// Returns rates that account for the specified delivery confirmation type. Default value: false
        /// </summary>
        [JsonProperty("residential")]
        public bool? IsResidential { get; set; }
    }

}
