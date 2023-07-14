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
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ShipStation4Net.Domain.Enumerations
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InsuranceOptionProviders
    {
        [EnumMember(Value = "shipsurance")]
        Shipsurance = 0,

        [EnumMember(Value = "carrier")]
        Carrier = 1,

        [EnumMember(Value = "provider")]
        Provider = 2,

        [EnumMember(Value = "xcover")]
        Xcover = 3,

        [EnumMember(Value = "parcelguard")]
        Parcelguard = 4,
    }
}
