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
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class InternationalOptions
    {
        /// <summary>
        /// Contents of international shipment. Available options are: "merchandise", "documents", "gift", "returned_goods", or "sample"
        /// </summary>
        [JsonProperty("contents")]
        public InternationalOptionContents? Contents { get; set; }

        /// <summary>
        /// An array of customs items. Please note: If you wish to supply customsItems in the CreateOrder call and have the values not be overwritten by ShipStation, you must have the International Settings > Customs Declarations set to "Leave blank (Enter Manually)" in the UI: https://ss.shipstation.com/#/settings/international
        /// </summary>
        [JsonProperty("customsItems")]
        public IList<CustomsItem> CustomsItems { get; set; }

        /// <summary>
        /// Non-Delivery option for international shipment. Available options are: "return_to_sender" or "treat_as_abandoned". Please note: If the shipment is created through the Orders/CreateLabelForOrder endpoint and the nonDelivery field is not specified then value defaults based on the International Setting in the UI. If the call is being made to the Shipments/CreateLabel endpoint and the nonDelivery field is not specified then the value will default to "return_to_sender".
        /// </summary>
        [JsonProperty("nonDelivery")]
        public InternationalOptionNonDelivery? NonDelivery { get; set; }
    }
}
