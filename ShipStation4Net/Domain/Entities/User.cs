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
    public class User
    {
        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Email address. This will also be the username of the account.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Password to set for account access.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("shippingOriginCountryCode")]
        public string ShippingOriginCountryCode { get; set; }

        /// <summary>
        /// Name of Company.
        /// </summary>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Company Address - Street 1
        /// </summary>
        [JsonProperty("addr1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Company Address - Street 2
        /// </summary>
        [JsonProperty("addr2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Company Address - City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Company Address - State
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Company Address - Zip Code
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Company Address - Country. Please use a 2-character country code.
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Company Phone number.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
    
}
