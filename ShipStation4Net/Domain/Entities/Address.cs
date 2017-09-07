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
    public class Address
    {
        /// <summary>
        /// Name of person.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of company.
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; }

        /// <summary>
        /// First line of address.
        /// </summary>
        [JsonProperty("street1")]
        public string Street1 { get; set; }

        /// <summary>
        /// Second line of address.
        /// </summary>
        [JsonProperty("street2")]
        public string Street2 { get; set; }

        /// <summary>
        /// Third line of address.
        /// </summary>
        [JsonProperty("street3")]
        public string Street3 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Postal Code
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country Code. The two-character ISO country code is required.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Telephone number.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Specifies whether the given address is residential.
        /// </summary>
        [JsonProperty("residential")]
        public bool? IsResidential { get; set; }

        /// <summary>
        /// Identifies whether the address has been verified by ShipStation (read only).
        /// Possible values: Address not yet validated, Address validated successfully, 
        /// Address validation warning, Address validation failed.
        /// </summary>
        [JsonProperty("addressVerified")]
        public AddressVerified? AddressVerified { get; set; }
    }
}
