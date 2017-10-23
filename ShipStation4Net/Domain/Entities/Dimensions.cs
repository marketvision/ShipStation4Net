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
    public class Dimensions
    {
        /// <summary>
        /// Length of package.
        /// </summary>
        [JsonProperty("length")]
        public double? Length { get; set; }

        /// <summary>
        /// Width of package.
        /// </summary>
        [JsonProperty("width")]
        public double? Width { get; set; }

        /// <summary>
        /// Height of package.
        /// </summary>
        [JsonProperty("height")]
        public double? Height { get; set; }

        /// <summary>
        /// Units of measurement. Allowed units are: "inches", or "centimeters"
        /// </summary>
        [JsonProperty("units")]
        public DimensionsUnits? Units { get; set; }
    }
}
