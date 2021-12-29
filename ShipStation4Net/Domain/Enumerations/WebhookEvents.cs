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
using ShipStation4Net.Converters;
using System.Runtime.Serialization;

namespace ShipStation4Net.Domain.Enumerations
{
    [JsonConverter(typeof(StringEnumConverterIgnoreUnknown))]
    public enum WebhookEvents
    {
        [EnumMember(Value = "ORDER_NOTIFY")]
        OrderNotify = 0,

        [EnumMember(Value = "ITEM_ORDER_NOTIFY")]
        ItemOrderNotify = 1,

        [EnumMember(Value = "SHIP_NOTIFY")]
        ShipNotify = 2,

        [EnumMember(Value = "ITEM_SHIP_NOTIFY")]
        ItemShipNotify = 3
    }
}
