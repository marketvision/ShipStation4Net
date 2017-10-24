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
using ShipStation4Net.Domain.Enumerations;

namespace ShipStation4Net.Domain.Entities
{
    public class InsuranceOptions
    {
        /// <summary>
        /// Preferred Insurance provider. Available options: "shipsurance" or "carrier"
        /// </summary>
        [JsonProperty("provider")]
        public InsuranceOptionProviders? Provider { get; set; }

        /// <summary>
        /// Indicates whether shipment should be insured.
        /// </summary>
        [JsonProperty("insureShipment")]
        public bool? InsureShipment { get; set; }

        /// <summary>
        /// Value to insure.
        /// </summary>
        [JsonProperty("insuredValue")]
        public double? InsuredValue { get; set; }
    }
}
